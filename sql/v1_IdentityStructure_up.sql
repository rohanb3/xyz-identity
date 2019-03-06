ALTER TABLE [TWC_Role] ADD [IsCustom] bit NOT NULL DEFAULT(0);

CREATE TABLE [Permissions] (
    [Id] uniqueidentifier NOT NULL,
    [Scope] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
);

CREATE TABLE [Policies] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Policies] PRIMARY KEY ([Id])
);

CREATE TABLE [PermissionToPolicy] (
    [PermissionId] uniqueidentifier NOT NULL,
    [PolicyId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_PermissionToPolicy] PRIMARY KEY ([PermissionId], [PolicyId]),
    CONSTRAINT [FK_PermissionToPolicy_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Permissions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PermissionToPolicy_Policies_PolicyId] FOREIGN KEY ([PolicyId]) REFERENCES [Policies] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [PolicyToRole] (
    [PolicyId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_PolicyToRole] PRIMARY KEY ([PolicyId], [RoleId]),
    CONSTRAINT [FK_PolicyToRole_Policies_PolicyId] FOREIGN KEY ([PolicyId]) REFERENCES [Policies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PolicyToRole_TWC_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [TWC_Role] ([RoleKey]) ON DELETE CASCADE
);

CREATE INDEX [IX_PermissionToPolicy_PolicyId] ON [PermissionToPolicy] ([PolicyId]);

CREATE INDEX [IX_PolicyToRole_RoleId] ON [PolicyToRole] ([RoleId]);
