//using System.Security.Claims;

//public class CurrentUserService
//{
//    private readonly IHttpContextAccessor _httpContextAccessor;

//    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
//    {
//        _httpContextAccessor = httpContextAccessor;
//    }

//    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

//    public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

//    public string FullName
//    {
//        get
//        {
//            var givenName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.GivenName)?.Value;
//            var surname = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Surname)?.Value;
//            return $"{surname} {givenName}".Trim();
//        }
//    }

//    public string AvatarUrl => _httpContextAccessor.HttpContext?.User?.FindFirst("AvatarUrl")?.Value ?? string.Empty;

//    public string Email => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

//    public string Role => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? "student";
//}