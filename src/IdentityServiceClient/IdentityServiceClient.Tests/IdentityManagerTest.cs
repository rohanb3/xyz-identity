using NUnit.Framework;
using IdentityServiceClient.Service;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using AutoFixture;
using IdentityServiceClient.Models.User;
using System.Net;
using System.Linq;
using System;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Specialized;
using FluentAssertions.Equivalency;

namespace IdentityServiceClient.Tests
{
    public class IdentityManagerTest
    {
        private IIdentityManager _identityManager;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _identityManager = new IdentityManager(new IdentityServiceClientOptions() { ServiceUrl = "https://localhost:8081/api" });
        }

        [Test]
        public async Task GetAllUsers_ReturnsNotEmptyPayload()
        {
            var response = await _identityManager.GetAllUsersAsync();
            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(response.Payload.Count != 0);
        }

        [Test]
        public async Task GetUserbyId()
        {
            // Arrange
            var allUsers = await _identityManager.GetAllUsersAsync();
            var random = new Random();
            var userIndex = random.Next(allUsers.Payload.Count - 1);
            var workingUser = allUsers.Payload[userIndex];
            // Act
            var user = await _identityManager.GetUserByIdAsync(workingUser.ObjectId);
            // Assert
            Assert.IsTrue(user.StatusCode == HttpStatusCode.OK);
            user.Payload.Should().NotBeNull();
        }

        [Test]
        public async Task GetAllUsersByRole()
        {
            // Arrange
            var allUsers = await _identityManager.GetAllUsersAsync();
            var random = new Random();
            var userIndex = random.Next(allUsers.Payload.Count - 1);
            var workingUser = allUsers.Payload[userIndex];
            // Act
            var response = await _identityManager.GetUsersByRole(workingUser.Role);
            // Assert
            Assert.IsTrue(response.Payload.FirstOrDefault(x => x.ObjectId == workingUser.ObjectId) != null);
        }

        [Test]
        public async Task GetAllUsersByManagerId()
        {
            // Arrange
            var allUsers = await _identityManager.GetAllUsersAsync();
            var random = new Random();
            var userIndex = random.Next(allUsers.Payload.Count - 1);
            var workingUser = allUsers.Payload[userIndex];
            // Act
            var response = await _identityManager.GetUsersByManager(workingUser.RetailerId.ToString());
            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            response.Payload.Should().NotBeNull();
        }

        //[Test]
        //public async Task CheckPermissionTest()
        //{
        //    await _identityManager.CheckPermissionExpiration();
        //    Assert.IsTrue(_identityManager.CheckPermission("TEST", new string[] { "TEST" }));
        //}
    }
}