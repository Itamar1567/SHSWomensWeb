using backend.Data;
public class DatabaseRepository
{
    private readonly AppDbContext _context;

    public DatabaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool TestConnection()
    {
        try
        {
            _context.Database.CanConnect();
            return true;
        }
        catch
        {
            return false;
        }

    }
}