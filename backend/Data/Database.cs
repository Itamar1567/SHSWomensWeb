using backend.Data;
using Microsoft.EntityFrameworkCore;
public class DatabaseRepository
{
    private readonly AppDbContext _db;

    public DatabaseRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> CreateNewsLetter(Newsletters newsletter)
    {
        try
        {
            await _db.Newsletters.AddAsync(newsletter);
            await _db.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<List<Newsletters>> GetNewsletters()
    {
        try
        {
            return await _db.Newsletters.OrderByDescending(n => n.created_at).ToListAsync();
        }
        catch
        {
            return new List<Newsletters>();
        }   
    }
    public bool TestConnection()
    {
        try
        {
            _db.Database.CanConnect();
            return true;
        }
        catch
        {
            return false;
        }

    }
}