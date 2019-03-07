use [db.xyzies.identity]

INSERT INTO [TWC_Role](RoleKey, [RoleId], [RoleName], [CreatedOn], IsCustom) VALUES('a1934803-c756-4bb0-9155-2003cfafb09a', 1, 'SuperAdmin', getutcdate(),0);
INSERT INTO [TWC_Role](RoleKey, [RoleId], [RoleName], [CreatedOn], IsCustom) VALUES('a1934803-c756-4bb0-9155-2003cfafb10a', 1, 'RetailerAdmin', getutcdate(),0);
INSERT INTO [TWC_Role](RoleKey, [RoleId], [RoleName], [CreatedOn], IsCustom) VALUES('a1934803-c756-4bb0-9155-2003cfafb11a', 1, 'SalesRep', getutcdate(),0);

INSERT INTO [Permissions]([Id], [Scope], [IsActive]) VALUES('a1934803-c756-4bb0-9155-2003cfafb02a', 'xyzies.reviews.reviews.read', 1);
INSERT INTO [Permissions]([Id], [Scope], [IsActive]) VALUES('a1934803-c756-4bb0-9155-2003cfafb03a', 'xyzies.reviews.reviews.write', 1);
INSERT INTO [Permissions]([Id], [Scope], [IsActive]) VALUES('a1934803-c756-4bb0-9155-2003cfafb04a', 'xyzies.reviews.reviews.update', 1);
INSERT INTO [Permissions]([Id], [Scope], [IsActive]) VALUES('a1934803-c756-4bb0-9155-2003cfafb05a', 'xyzies.reviews.reviews.delete', 1);

INSERT INTO [Permissions]([Id], [Scope], [IsActive]) VALUES('a1934803-c756-4bb0-9155-2003cfafb06a', 'xyzies.reviews.templates.read', 1);
INSERT INTO [Permissions]([Id], [Scope], [IsActive]) VALUES('a1934803-c756-4bb0-9155-2003cfafb07a', 'xyzies.reviews.templates.write', 1);
INSERT INTO [Permissions]([Id], [Scope], [IsActive]) VALUES('a1934803-c756-4bb0-9155-2003cfafb08a', 'xyzies.reviews.templates.update', 1);
INSERT INTO [Permissions]([Id], [Scope], [IsActive]) VALUES('a1934803-c756-4bb0-9155-2003cfafb09a', 'xyzies.reviews.templates.delete', 1);

INSERT INTO [Policies]([Id], [Name]) VALUES('6f7ba6cc-df01-40c8-81b0-f2eccbeeffd8', 'ReviewsFull');
INSERT INTO [Policies]([Id], [Name]) VALUES('6f7ba6cc-df01-40c8-81b0-f2eccbeeffd9', 'TemplatesFull');

INSERT INTO [PermissionToPolicy] ([PermissionId], [PolicyId]) VALUES('a1934803-c756-4bb0-9155-2003cfafb02a', '6f7ba6cc-df01-40c8-81b0-f2eccbeeffd8');
INSERT INTO [PermissionToPolicy] ([PermissionId], [PolicyId]) VALUES('a1934803-c756-4bb0-9155-2003cfafb03a', '6f7ba6cc-df01-40c8-81b0-f2eccbeeffd8');
INSERT INTO [PermissionToPolicy] ([PermissionId], [PolicyId]) VALUES('a1934803-c756-4bb0-9155-2003cfafb04a', '6f7ba6cc-df01-40c8-81b0-f2eccbeeffd8');
INSERT INTO [PermissionToPolicy] ([PermissionId], [PolicyId]) VALUES('a1934803-c756-4bb0-9155-2003cfafb05a', '6f7ba6cc-df01-40c8-81b0-f2eccbeeffd8');

INSERT INTO [PermissionToPolicy] ([PermissionId], [PolicyId]) VALUES('a1934803-c756-4bb0-9155-2003cfafb06a', '6f7ba6cc-df01-40c8-81b0-f2eccbeeffd9');
INSERT INTO [PermissionToPolicy] ([PermissionId], [PolicyId]) VALUES('a1934803-c756-4bb0-9155-2003cfafb07a', '6f7ba6cc-df01-40c8-81b0-f2eccbeeffd9');
INSERT INTO [PermissionToPolicy] ([PermissionId], [PolicyId]) VALUES('a1934803-c756-4bb0-9155-2003cfafb08a', '6f7ba6cc-df01-40c8-81b0-f2eccbeeffd9');
INSERT INTO [PermissionToPolicy] ([PermissionId], [PolicyId]) VALUES('a1934803-c756-4bb0-9155-2003cfafb09a', '6f7ba6cc-df01-40c8-81b0-f2eccbeeffd9');


INSERT INTO [PolicyToRole] ([PolicyId], [RoleId]) VALUES('6f7ba6cc-df01-40c8-81b0-f2eccbeeffd9', 'a1934803-c756-4bb0-9155-2003cfafb09a');
INSERT INTO [PolicyToRole] ([PolicyId], [RoleId]) VALUES('6f7ba6cc-df01-40c8-81b0-f2eccbeeffd9', 'a1934803-c756-4bb0-9155-2003cfafb11a');

INSERT INTO [PolicyToRole] ([PolicyId], [RoleId]) VALUES('6f7ba6cc-df01-40c8-81b0-f2eccbeeffd8', 'a1934803-c756-4bb0-9155-2003cfafb09a');
INSERT INTO [PolicyToRole] ([PolicyId], [RoleId]) VALUES('6f7ba6cc-df01-40c8-81b0-f2eccbeeffd8', 'a1934803-c756-4bb0-9155-2003cfafb11a');

