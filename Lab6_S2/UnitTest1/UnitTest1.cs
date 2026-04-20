using UnitTest1;

namespace UnitTest2
{
    public class UnitTest1
    {

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        // 2. Pomocnicza klasa z właściwością "NazwaKlasyId" (PersonId)
        public class Person
        {
            public int PersonId { get; set; }
            public string FullName { get; set; }
        }

        public class FakeRepositoryTests
        {
            [Fact]
            public void Add_WhenCalledWithValidEntity_AddsToRepository()
            {
                // Arrange
                var repository = new FakeRepository<Product>();
                var product = new Product { Id = 1, Name = "Laptop" };

                // Act
                repository.Add(product);
                var allItems = repository.GetAll().ToList();

                // Assert
                Assert.Single(allItems);
                Assert.Equal("Laptop", allItems.First().Name);
            }

            [Fact]
            public void Add_WhenCalledWithNull_DoesNotThrowAndDoesNotAdd()
            {
                // Arrange
                var repository = new FakeRepository<Product>();

                // Act
                repository.Add(null);
                var allItems = repository.GetAll().ToList();

                // Assert
                Assert.Empty(allItems);
            }

            [Fact]
            public void GetById_WithStandardIdProperty_ReturnsCorrectEntity()
            {
                // Arrange
                var repository = new FakeRepository<Product>();
                repository.Add(new Product { Id = 1, Name = "Laptop" });
                repository.Add(new Product { Id = 2, Name = "Myszka" });

                // Act
                var result = repository.GetById(2);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Id);
                Assert.Equal("Myszka", result.Name);
            }

            [Fact]
            public void GetById_WithTypeNameIdProperty_ReturnsCorrectEntity()
            {
                // Arrange
                var repository = new FakeRepository<Person>();
                repository.Add(new Person { PersonId = 1, FullName = "Jan Kowalski" });
                repository.Add(new Person { PersonId = 2, FullName = "Anna Nowak" });

                // Act
                var result = repository.GetById(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.PersonId);
                Assert.Equal("Jan Kowalski", result.FullName);
            }

            [Fact]
            public void GetById_WhenEntityDoesNotExist_ReturnsNull()
            {
                // Arrange
                var repository = new FakeRepository<Product>();
                repository.Add(new Product { Id = 1, Name = "Laptop" });

                // Act
                var result = repository.GetById(99);

                // Assert
                Assert.Null(result);
            }

            [Fact]
            public void Delete_WhenEntityExists_RemovesFromRepository()
            {
                // Arrange
                var repository = new FakeRepository<Product>();
                repository.Add(new Product { Id = 1, Name = "Laptop" });
                repository.Add(new Product { Id = 2, Name = "Myszka" });

                // Act
                repository.Delete(1);
                var remainingItems = repository.GetAll().ToList();

                // Assert
                Assert.Single(remainingItems);
                Assert.Equal(2, remainingItems.First().Id);
            }

            [Fact]
            public void Delete_WhenEntityDoesNotExist_DoesNothingAndDoesNotThrow()
            {
                // Arrange
                var repository = new FakeRepository<Product>();
                repository.Add(new Product { Id = 1, Name = "Laptop" });

                // Act
                var exception = Record.Exception(() => repository.Delete(99));
                var remainingItems = repository.GetAll().ToList();

                // Assert
                Assert.Null(exception); // Nie rzuca błędu
                Assert.Single(remainingItems); // Obiekt z Id=1 nadal tam jest
            }

            [Fact]
            public void GetAll_ReturnsAllEntitiesAsQueryable()
            {
                // Arrange
                var repository = new FakeRepository<Product>();
                repository.Add(new Product { Id = 1, Name = "A" });
                repository.Add(new Product { Id = 2, Name = "B" });
                repository.Add(new Product { Id = 3, Name = "C" });

                // Act
                var queryableResult = repository.GetAll();
                var listResult = queryableResult.Where(p => p.Id > 1).ToList();

                // Assert
                Assert.NotNull(queryableResult);
                Assert.IsAssignableFrom<IQueryable<Product>>(queryableResult);
                Assert.Equal(3, queryableResult.Count());
                Assert.Equal(2, listResult.Count);
            }

            [Fact]
            public void Update_DoesNotThrowAndReflectsInMemoryChanges()
            {
                // Arrange
                var repository = new FakeRepository<Product>();
                var product = new Product { Id = 1, Name = "Stara Nazwa" };
                repository.Add(product);

                // Act
                // W pamięci wystarczy zmienić referencję
                var entityToUpdate = repository.GetById(1);
                entityToUpdate.Name = "Nowa Nazwa";

                // Wywołanie Update (nawet jeśli puste) nie powinno rzucać błędu
                var exception = Record.Exception(() => repository.Update(entityToUpdate));

                var checkEntity = repository.GetById(1);

                // Assert
                Assert.Null(exception);
                Assert.Equal("Nowa Nazwa", checkEntity.Name);
            }
        }
    }
}
