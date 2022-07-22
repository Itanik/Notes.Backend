namespace Notes.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(NotesDbContext context)
        {
            context.Database.EnsureCreated(); // TODO: заменить на context.Database.Migrate() для поддержки миграций
        }
    }
}
