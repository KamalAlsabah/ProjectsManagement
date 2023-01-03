using ProjectsManagement.Debugging;

namespace ProjectsManagement
{
    public class ProjectsManagementConsts
    {
        public const string LocalizationSourceName = "ProjectsManagement";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "eac4c2f901e34f268268f531405e125b";
    }
}
