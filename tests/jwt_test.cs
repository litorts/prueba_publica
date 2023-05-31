using crecer_backend.Bussines;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;
using System;
namespace crecer_backend.tests
{
    public class JwtTokenTests
    {
        [Test]
        public void GenerateAndValidateJwtToken()
        {
            // Arrange
            var expectedRut = "19246931-7";
            var jwtSecret = FunctionBussines.get_jwt_secret();

            TestContext.WriteLine($"jwtSecret: {jwtSecret}");

            // Act
            var token = FunctionBussines.generate_jwtoken(expectedRut);

            TestContext.WriteLine($"token: {token}");

            var actualRut = FunctionBussines.get_rut_from_jwtoken(token);

            TestContext.WriteLine($"actualRut: {actualRut}");

            // Assert
            Assert.IsNotNull(token);
            Assert.IsNotNull(actualRut);
            Assert.AreEqual(expectedRut, actualRut);
        }

    }
}
