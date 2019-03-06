use timewarner_20181026

INSERT INTO [TWC_Role](RoleKey, [RoleId], [RoleName]) VALUES('a1934803-c756-4bb0-9155-2003cfafb10a', 'xyzies.sso.identity.full', 1);
INSERT INTO [Permissions]([Id], [Scope], [IsActive]) VALUES('a1934803-c756-4bb0-9155-2003cfafb10a', 'xyzies.sso.identity.full', 1);
INSERT INTO [Policies]([Id], [Name]) VALUES('6f7ba6cc-df01-40c8-81b0-f2eccbeeffd8', 'Test Policy');

INSERT INTO [PermissionToPolicy] ([PermissionId], [PolicyId]) VALUES('a1934803-c756-4bb0-9155-2003cfafb10a', '6f7ba6cc-df01-40c8-81b0-f2eccbeeffd8');
INSERT INTO [PolicyToRole] ([PolicyId], [RoleId]) VALUES('6f7ba6cc-df01-40c8-81b0-f2eccbeeffd8', '4A0764CD-7EE5-4205-98AA-BF729B4A03F3');
