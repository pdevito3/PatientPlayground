namespace PatientManagement.UnitTests.UnitTests.Domain.RolePermissions.Features;

using PatientManagement.SharedTestHelpers.Fakes.RolePermission;
using PatientManagement.Domain.RolePermissions;
using PatientManagement.Domain.RolePermissions.Dtos;
using PatientManagement.Domain.RolePermissions.Mappings;
using PatientManagement.Domain.RolePermissions.Features;
using PatientManagement.Domain.RolePermissions.Services;
using MapsterMapper;
using FluentAssertions;
using HeimGuard;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetRolePermissionListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IRolePermissionRepository> _rolePermissionRepository;
    private readonly Mock<IHeimGuardClient> _heimGuard;

    public GetRolePermissionListTests()
    {
        _rolePermissionRepository = new Mock<IRolePermissionRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
        _heimGuard = new Mock<IHeimGuardClient>();
    }
    
    [Test]
    public async Task can_get_paged_list_of_rolePermission()
    {
        //Arrange
        var fakeRolePermissionOne = FakeRolePermission.Generate();
        var fakeRolePermissionTwo = FakeRolePermission.Generate();
        var fakeRolePermissionThree = FakeRolePermission.Generate();
        var rolePermission = new List<RolePermission>();
        rolePermission.Add(fakeRolePermissionOne);
        rolePermission.Add(fakeRolePermissionTwo);
        rolePermission.Add(fakeRolePermissionThree);
        var mockDbData = rolePermission.AsQueryable().BuildMock();
        
        var queryParameters = new RolePermissionParametersDto() { PageSize = 1, PageNumber = 2 };

        _rolePermissionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetRolePermissionList.Query(queryParameters);
        var handler = new GetRolePermissionList.Handler(_rolePermissionRepository.Object, _mapper, _sieveProcessor, _heimGuard.Object);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }
}