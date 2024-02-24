using BlogWebApp.Data;
using BlogWebApp.Interfaces;
using BlogWebApp.Models;
using BlogWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogWebApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IExcelService _excelService;

        public PostController(IPostRepository postRepository, ICommentRepository commentRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor, IExcelService excelService) 
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
            _excelService = excelService;
        }
        public async Task<IActionResult> Index(int category = -1, string? keyword = null, int page = 1, int pageSize = 5)
        {
            if (page < 1 || pageSize < 1)
            {
                return NotFound();
            }

            var posts = (string.IsNullOrEmpty(keyword))
                ? (category == -1
                    ? await _postRepository.GetSliceAsync((page - 1) * pageSize, pageSize)
                    : await _postRepository.GetPostsByCategoryAndSliceAsync((Category)category, (page - 1) * pageSize, pageSize))
                : await _postRepository.GetPostsByKeywordAndSliceAsync(keyword, (page - 1) * pageSize, pageSize);

            var count = (string.IsNullOrEmpty(keyword))
                ? (category == -1
                    ? await _postRepository.GetCountAsync()
                    : await _postRepository.GetCountByCategoryAsync((Category)category))
                : await _postRepository.GetCountByKeywordAsync(keyword);

            var postViewModel = new IndexPostViewModel
            {
                Posts = posts,
                Page = page,
                PageSize = pageSize,
                TotalPosts = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Category = category,
                Keyword = keyword
            };

            return View(postViewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Post post = await _postRepository.GetByIdAsync(id);
            return View(post);
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createPostViewModel = new CreatePostViewModel 
            {
                AppUserId = curUserId
            };
            return View(createPostViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel postVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(postVM.ImageUrl);
                var post = new Post
                {
                    Title = postVM.Title,
                    Description = postVM.Description,
                    ImageUrl = result.Url.ToString(),
                    Category=postVM.Category,
                    AppUserId = postVM.AppUserId
                };
                _postRepository.Add(post);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(postVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return View("Error");
            var postVM = new EditPostViewModel
            {
                Title = post.Title,
                Description = post.Description,
                URL = post.ImageUrl,
                Category = post.Category,
                CreatedDate = post.CreatedDate
            };
            return View(postVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPostViewModel postVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit post");
                return View("Edit", postVM);
            }

            var userPost = await _postRepository.GetByIdAsyncNoTracking(id);

            if (userPost == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(postVM.ImageUrl);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(postVM);
            }

            if (!string.IsNullOrEmpty(userPost.ImageUrl))
            {
                _ = _photoService.DeletePhotoAsync(userPost.ImageUrl);
            }

            var post = new Post
            {
                Id = id,
                Title = postVM.Title,
                Description = postVM.Description,
                ImageUrl = photoResult.Url.ToString(),
                Category = postVM.Category,
                CreatedDate = userPost.CreatedDate,
                AppUserId = userPost.AppUserId
            };

            _postRepository.Update(post);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var postDetails = await _postRepository.GetByIdAsync(id);
            if(postDetails==null) return View("Error");
            return View(postDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var postDetails = await _postRepository.GetByIdAsync(id);
            if (postDetails == null) return View("Error");

            var postComments = await _commentRepository.GetCommentsByPostId(id);
            foreach (var comment in postComments)
            {
                _commentRepository.Delete(comment);
            }

            _postRepository.Delete(postDetails);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int postId, string text, string userId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null) return NotFound("Comment not found");
            if (string.IsNullOrEmpty(userId))
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    PostId = postId,
                    AppUserId = userId,
                    Text = text
                };
                _commentRepository.Add(comment);
                return Json(comment);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        public async Task<IActionResult> DownloadExcel()
        {
            var posts = await _postRepository.GetAll();
            var excelData = _excelService.GenerateExcelFile(posts);

            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BlogPosts.xlsx");
        }
    }
}       
