namespace BusinessLayer.Models.Response
{
    public class LoginResponseModel
    {
        public TokenModel Token { get; set; }
        public UserResponseModel User { get; set; }
    }
}
