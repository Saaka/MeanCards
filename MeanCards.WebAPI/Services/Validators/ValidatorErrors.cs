namespace MeanCards.WebAPI.Services.Validators
{
    public static class ValidatorErrors
    {
        public const string DuplicatedEmail = nameof(DuplicatedEmail);
        public const string NameRequired = nameof(NameRequired);
        public const string EmailRequired = nameof(EmailRequired);
        public const string PasswordRequired = nameof(PasswordRequired);
        public const string PasswordTooShort = nameof(PasswordTooShort);
        public const string DuplicatedUserName = nameof(DuplicatedUserName);
    }
}

