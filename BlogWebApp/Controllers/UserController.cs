using BlogWebApp.Data;
using BlogWebApp.Interfaces;
using BlogWebApp.Models;
using BlogWebApp.Repository;
using BlogWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _userManager = userManager;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Role = userRole.First(),
                    Email = user.Email
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }
            var userRole = await _userManager.GetRolesAsync(user);
            var availableRoles = _userRepository.GetUserRoles();
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Role = userRole.First(),
                AvailableRoles = availableRoles,
                Email = user.Email
            };
            return View(userDetailViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserDetailViewModel editVM)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to change role.");
                return View("Detail", editVM);
            }

            var user = await _userRepository.GetUserById(editVM.Id);

            if (user == null)
            {
                return View("Error");
            }

            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (editVM.Role != currentRole)
            {
                await _userManager.RemoveFromRoleAsync(user, currentRole);
                
                await _userManager.AddToRoleAsync(user, editVM.Role);

                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("Detail", "User", new { id = editVM.Id });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string userId)
        {
            var userToDelete = await _userRepository.GetUserById(userId);

            if (userToDelete == null)
            {
                return NotFound("User Not Found!");
            }

            return View("Delete", userToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string userId)
        {
            var userToDelete = await _userRepository.GetUserById(userId);

            if (userToDelete == null)
            {
                return NotFound("User Not Found!");
            }

            var userComments = await _commentRepository.GetCommentsByUserId(userId);
            foreach (var comment in userComments)
            {
                _commentRepository.Delete(comment);
            }

            var userPosts = await _postRepository.GetPostsByUserId(userId);
            foreach (var post in userPosts)
            {
                _postRepository.Delete(post);
            }

            if (!_userRepository.Delete(userToDelete))
            {
                return StatusCode(500, "Error deleting user");
            }

            return RedirectToAction("Index");
        }

    }
}
