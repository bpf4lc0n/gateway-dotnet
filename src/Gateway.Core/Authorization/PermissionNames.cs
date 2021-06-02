namespace Gateway.Authorization
{
    public static class PermissionNames
    {
        public const string Pages_Users = "Pages.Users";

        public const string Pages_GlobalSettings = "Pages.GlobalSettings";

        public const string Func_ChangeApplicationSettings = "Func.ChangeApplicationSettings";

        public static bool IsPermissionChangeApplicationSettings(string permissionName)
        {
            return permissionName == Func_ChangeApplicationSettings;
        }
    }
}
