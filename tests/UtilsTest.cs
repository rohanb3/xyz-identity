﻿using System.Security.Policy;
using graphApiService.Dtos.User;
using Xunit;
using graphApiService.Services;
using tests.Services;

namespace tests
{
    public class UtilsTest
    {
        private readonly GraphClientServiceMock _graphClient;
        public UtilsTest()
        {
            _graphClient = new GraphClientServiceMock();
        }
        [Fact]
        public async void MergeObjects_Returns_DestinationObjectWithChangedProperties()
        {
            var userEditableDto = new UserProfileEditableDto()
            {
                AccountEnabled = false,
                GivenName = "Changed",
                AvatarUrl = new Url("https://changed.com"),
                RetailerId = 200
            };

            var userToChange = await _graphClient.GetUserByObjectId("2");

            Utils.MergeObjects(userEditableDto, userToChange);

            Assert.True(userToChange.AccountEnabled == userEditableDto.AccountEnabled &&
                        userToChange.GivenName == userEditableDto.GivenName &&
                        userToChange.AvatarUrl.Equals(userEditableDto.AvatarUrl) &&
                        userToChange.RetailerId == userEditableDto.RetailerId);
        }
    }
}
