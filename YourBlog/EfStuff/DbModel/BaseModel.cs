namespace YourBlog.EfStuff.DbModel
{
    public abstract class BaseModel
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
    }
}
