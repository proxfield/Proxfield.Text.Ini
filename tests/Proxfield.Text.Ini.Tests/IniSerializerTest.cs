using Proxfield.Text.Ini.Tests.FakeModels;

namespace Proxfield.Text.Ini.Tests
{
    public class IniSerializerTest
    {
        [Fact]
        public void Deserialize_ShouldBeOk()
        {
            //Arrange
            var expected = new FakeUser()
            {
                Age = 10,
                Name = "Jose"
            };
            string content = $"[FakeUser]\nAge=10\nName=Jose";
            //Act
            var result = IniSerializer.DeserializeObject<FakeUser>(content);
            //Assert
            Assert.Equal(expected.Name, result?.Name);
            Assert.Equal(expected.Age, result?.Age);
        }

        [Fact]
        public void Serialize_ShouldBeOk()
        {
            //Arrange
            string expected = $"[FakeUser]\nName=Jose\r\nAge=10\r\n";
            var user = new FakeUser()
            {
                Age = 10,
                Name = "Jose"
            };
            //Act
            var result = IniSerializer.SerializeObject<FakeUser>(user);
            //Assert
            Assert.Equal(expected, result);
        }
    }
}