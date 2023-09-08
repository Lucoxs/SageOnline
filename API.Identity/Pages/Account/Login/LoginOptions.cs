namespace UI.Pages.Login;

public class LoginOptions
{
    public static bool AllowLocalLogin = true;
    public static bool AllowRememberLogin = true;
    public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
    public static string InvalidCredentialsErrorMessage = "Invalid username or password";
    public static string InvalidUsernameErrorMessage = "Email invalide !";
    public static string InvalidPasswordErrorMessage = "Mot de passe invalide !";
    public static string InvalidScopeErrorMessage = "Aucun accès définit pour cette utilisateur !";
}