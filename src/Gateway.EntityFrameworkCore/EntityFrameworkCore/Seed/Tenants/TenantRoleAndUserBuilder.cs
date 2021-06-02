﻿using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Gateway.Authorization;
using Gateway.Authorization.Roles;
using Gateway.Authorization.Users;

namespace Gateway.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly GatewayDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(GatewayDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            // Admin roles

            var superadminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.SuperAdmin);
            var adminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);

            if (superadminRole == null)
            {
                superadminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.SuperAdmin, StaticRoleNames.Tenants.SuperAdmin) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true, IsDefault = true }).Entity;
                _context.SaveChanges();
            }

            // Grant all permissions to superadmin role

            var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == _tenantId && p.RoleId == superadminRole.Id)
                .Select(p => p.Name)
                .ToList();

            var permissions = PermissionFinder
                .GetAllPermissions(new GatewayAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = superadminRole.Id
                    })
                );
                _context.SaveChanges();
            }

            // Grant permissions to admin role

            var ignorePermissions = _context.Permissions.IgnoreQueryFilters()
               .OfType<RolePermissionSetting>()
               .Where(p => p.TenantId == _tenantId && p.RoleId == superadminRole.Id && p.Name == PermissionNames.Func_ChangeApplicationSettings)
               .Select(p => p.Name)
               .ToList();

            var allpermissions = PermissionFinder
                .GetAllPermissions(new GatewayAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !ignorePermissions.Contains(p.Name))
                .ToList();

            if (allpermissions.Any())
            {
                _context.Permissions.AddRange(
                    allpermissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = adminRole.Id
                    })
                );
                _context.SaveChanges();
            }

            // Admin user

            var adminUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "Gateway01@gmail.com");
                adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "Gateway1!");
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, superadminRole.Id));
                _context.SaveChanges();
            }
        }
    }
}
