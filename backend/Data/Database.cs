using backend.Data;
using Microsoft.EntityFrameworkCore;
public class DatabaseRepository
{
    private readonly AppDbContext _db;

    public DatabaseRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> IsDuplicateTitle(string value)
    {
        return await _db.Newsletters.AnyAsync(n => n.title == value);
    }

    //Check if a title exists in any row apart from id
    public async Task<bool> IsDuplicateTitle(string value, int idToNotInclude)
    {
        return await _db.Newsletters.Where(n => n.id != idToNotInclude).AnyAsync(n => n.title == value);
    }

    public async Task<bool> IsDuplicateImage(string value)
    {
        return await _db.StoredImages
            .AnyAsync(n => n.image_path == value);
    }

    public async Task<bool> InsertImagePathToImageTable(string path)
    {
        try
        {
            StoredImages storedImages = new StoredImages{image_path = path};
            await _db.StoredImages.AddAsync(storedImages); 
            await _db.SaveChangesAsync();
            return true;

        }catch(Exception ex)
        {
            Console.WriteLine("Unable to add image to image table: " + ex);
            return false;
        }
        
        
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

    public async Task<EditNewsletterDTO> GetNewsletterByIdFromDatabase(int id)
    {
        try
        {
            var newsletter = await _db.Newsletters.Where(n => n.id == id).Select(n => new EditNewsletterDTO
            {
                id = n.id,
                title = n.title,
                slug = n.slug,
                author = n.author,
                image_path = n.image_path != null ? n.image_path : null,
                short_description = n.short_description ?? null,
                story_text = n.story_text
            }).FirstOrDefaultAsync();

            if(newsletter == null)
            {
                throw new Exception("Could not find a newsletter with the given id");
            }
            else
            {
                return newsletter;
            }

        }catch(Exception ex)
        {
            Console.WriteLine("Issue when getting newsletter from database ", ex);
            throw;
        }
    }

    public async Task<bool> OverrideNewsletterByIdFromDatabase(EditNewsletterDTO editedNewsletter)
    {
        try
        {
            var oldNewsletter = await _db.Newsletters.Where(n => n.id == editedNewsletter.id).FirstOrDefaultAsync();

            if(oldNewsletter == null)
            {
                throw new Exception("Newsletter to overwrite not found");
            }

            oldNewsletter.title = editedNewsletter.title;
            oldNewsletter.slug = editedNewsletter.slug;
            oldNewsletter.author = editedNewsletter.author;
            oldNewsletter.short_description = editedNewsletter.short_description;
            oldNewsletter.image_path = editedNewsletter.image_path;
            oldNewsletter.story_text = editedNewsletter.story_text;

            await _db.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            throw new Exception("Failed to connect to the database: " + ex);
        }
    }

    public async Task<List<GetNewsLetterDTO>> GetNewsletters()
    {
        try
        {
            return await _db.Newsletters
            .OrderByDescending(n => n.created_at)
            .Select(n => new GetNewsLetterDTO
            {
                id = n.id,
                title = n.title,
                slug = n.slug,
                author = n.author,
                image_path = n.image_path != null ? "https://storage.googleapis.com/shs_newsletter_images/" + n.image_path : null,
                short_description = n.short_description ?? null,
                story_text = n.story_text,
                created_at = n.created_at,
                updated_at = n.updated_at
            })
            .ToListAsync();


        }
        catch
        {
            return new List<GetNewsLetterDTO>();
        }
    }

    public async Task<string> DeleteNewsLetter(int id)
    {
        try
        {
            var newsletter = await _db.Newsletters.FindAsync(id);

            if (newsletter == null)
            {
                throw new Exception("Newsletter not found");
            }

            _db.Newsletters.Remove(newsletter);
            await _db.SaveChangesAsync();
            return "Newsletter deleted successfully";
        }
        catch
        {
            throw new Exception("Failed to delete newsletter");
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