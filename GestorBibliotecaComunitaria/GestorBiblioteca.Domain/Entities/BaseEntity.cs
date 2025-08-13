using MongoDB.Bson.Serialization.Attributes;

namespace GestorBiblioteca.Domain.Entities
{
    public  class BaseEntity
    {
        protected BaseEntity()
        {
            CreatedAt = DateTime.Now;
            IsDeleted = false;
        }
        [BsonId]
        [BsonElement("id")]
        public int Id { get; private set; }
        [BsonElement("create_at")]
        public DateTime CreatedAt { get; private set; }
        [BsonElement("is_deleted")]
        public bool IsDeleted { get; private set; }

        public void SetAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
