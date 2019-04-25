delete from [Permissions];
delete from [Policies];
delete from [TWC_Role];

insert into [Permissions] values ('814615e0-7785-4396-9f0d-b3715a135b9b', 'xyzies.reviews.templates.read', 1);  	 --0
insert into [Permissions] values ('40ea2f94-1214-4c2c-aa8e-6db0175e798b', 'xyzies.reviews.templates.write', 1);		 --1
insert into [Permissions] values ('74338e7a-b8bd-4c22-ab27-22c31a41884d', 'xyzies.reviews.templates.update', 1);	 --2
insert into [Permissions] values ('f617f927-f190-48d1-ba9c-6edcaea30732', 'xyzies.reviews.templates.delete', 1);	 --3
																													 
insert into [Permissions] values ('e2365aef-552e-4a5b-b0e2-6eee5a5a444e', 'xyzies.reviews.reviews.read', 1);  		 --4
insert into [Permissions] values ('2e19850a-1e89-49b4-9a18-60f02a7639f1', 'xyzies.reviews.reviews.write', 1);		 --5
insert into [Permissions] values ('418b0827-0a18-4f24-a9ae-85bd473d5abc', 'xyzies.reviews.reviews.update', 1);		 --6
insert into [Permissions] values ('66dfa9f7-8c86-48e0-9ec5-7e382729985c', 'xyzies.reviews.reviews.delete', 1);		 --7
																													 
insert into [Permissions] values ('63e781cc-7637-4d07-bed0-5629d5da92ec', 'xyzies.authorization.reviews.admin', 1);  --8
insert into [Permissions] values ('3a8a3a6c-4b9f-4425-81f1-476cddd436da', 'xyzies.authorization.reviews.mobile', 1); --9

insert into [Permissions] values ('96bf3985-0536-4bea-9bb1-8c71a86da6fc', 'xyzies.authorization.vsp.operator', 1);	 --10
insert into [Permissions] values ('cace7e20-c5f9-4e98-984d-76fd8c35c4d5', 'xyzies.authorization.vsp.mobile', 1);	 --11

insert into [Policies] values ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'TemplatesFull'); --0
insert into [Policies] values ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'ReviewsFull'); --1
insert into [Policies] values ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'ReviewsAdminLogin'); --2
insert into [Policies] values ('71f11476-42ff-4d4e-a05e-9a4e3fd45274', 'ReviewsMobileLogin'); --3
insert into [Policies] values ('b695018c-c264-4246-9e33-9dce90f338c2', 'VspOperatorLogin'); --4
insert into [Policies] values ('9ffce075-3299-4227-aae3-859f3c6e9eb6', 'VspMobileLogin'); --5

insert into [TWC_Role] values ('cb308b73-acf0-4f23-89b0-509b6bc0e7e6', 1, 'SuperAdmin', NULL,  getdate(), 0); --1
insert into [TWC_Role] values ('a89d3c96-5f4d-475f-8588-08e1523feffb', 2, 'SalesRep', NULL,  getdate(), 0); --2
insert into [TWC_Role] values ('c71b2170-98cf-4e69-8455-282fdbd21779', 3, 'Agent', NULL,  getdate(), 0); --3
insert into [TWC_Role] values ('87a421a2-60e1-46c9-8e2e-679c1f5f3c8e', 4, 'SupportAdmin', NULL,  getdate(), 0); --4
insert into [TWC_Role] values ('a2285edf-44d0-4f2b-be30-4d6e49644da2', 5, 'SystemAdmin', NULL,  getdate(), 0); --5
insert into [TWC_Role] values ('fafbb1b8-b039-4145-a923-f1c39d4fe603', 6, 'Manager', NULL,  getdate(), 0); --6
insert into [TWC_Role] values ('37fdfbf6-3ee2-4827-b7be-cefe78213d92', 7, 'Supervisor', NULL,  getdate(), 0); --7
insert into [TWC_Role] values ('ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9', 8, 'AccountAdmin', NULL,  getdate(), 0); --8
insert into [TWC_Role] values ('da671d01-1133-4cc9-94a6-b77587f21fad', 9, 'OperationAdmin', NULL, getdate(), 0); --9
insert into [TWC_Role] values ('4d6de41d-bfdb-4a68-8988-017c2a4ce2af', 10, 'Role #10', NULL, getdate(), 0); --10
insert into [TWC_Role] values ('af16483d-9c81-4443-84d6-a976e54fbfa9', 11, 'Role #11', NULL, getdate(), 0); --11
insert into [TWC_Role] values ('1727f429-580a-4d6b-b3fe-bce0ebbbd210', 12, 'Role #12', NULL, getdate(), 0); --12
insert into [TWC_Role] values ('13e75147-0736-45db-bf34-d3b0aebdeafe', 13, 'Role #13', NULL, getdate(), 0); --13
insert into [TWC_Role] values ('d47fd2ad-46ce-4173-8689-88573aa9ffac', 14, 'Role #14', NULL, getdate(), 0); --14
insert into [TWC_Role] values ('1e0b4f66-d15e-4366-994a-5bba45db1540', 15, 'Role #15', NULL, getdate(), 0); --15
insert into [TWC_Role] values ('b2669957-2b49-460b-9074-3a68aa075c12', 16, 'Role #16', NULL, getdate(), 0); --16
insert into [TWC_Role] values ('384c5edb-9db2-4cc5-bada-6b265a61ae6c', 17, 'Operator', NULL, getdate(), 0); --17
insert into [TWC_Role] values ('a98dc9cb-7847-4858-8693-5fd11b24b58c', 0, 'Anonymous', NULL, getdate(), 0); --0

insert into [PermissionToPolicy] values ('814615e0-7785-4396-9f0d-b3715a135b9b', '415b2993-7e32-4859-8b59-2b527bcdeea1'); --templates permissions to Templates Full Policy
insert into [PermissionToPolicy] values ('40ea2f94-1214-4c2c-aa8e-6db0175e798b', '415b2993-7e32-4859-8b59-2b527bcdeea1'); --templates permissions to Templates Full Policy
insert into [PermissionToPolicy] values ('74338e7a-b8bd-4c22-ab27-22c31a41884d', '415b2993-7e32-4859-8b59-2b527bcdeea1'); --templates permissions to Templates Full Policy
insert into [PermissionToPolicy] values ('f617f927-f190-48d1-ba9c-6edcaea30732', '415b2993-7e32-4859-8b59-2b527bcdeea1'); --templates permissions to Templates Full Policy

insert into [PermissionToPolicy] values ('e2365aef-552e-4a5b-b0e2-6eee5a5a444e', '6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2'); --reviews permissions to Reviews Full Policy
insert into [PermissionToPolicy] values ('2e19850a-1e89-49b4-9a18-60f02a7639f1', '6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2'); --reviews permissions to Reviews Full Policy
insert into [PermissionToPolicy] values ('418b0827-0a18-4f24-a9ae-85bd473d5abc', '6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2'); --reviews permissions to Reviews Full Policy
insert into [PermissionToPolicy] values ('66dfa9f7-8c86-48e0-9ec5-7e382729985c', '6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2'); --reviews permissions to Reviews Full Policy

insert into [PermissionToPolicy] values ('63e781cc-7637-4d07-bed0-5629d5da92ec', '91d3b70e-3c7e-4faf-97c0-718809bf3a2a'); --authorization.reviews.admin permissions to Reviews Admin Login
insert into [PermissionToPolicy] values ('3a8a3a6c-4b9f-4425-81f1-476cddd436da', '71f11476-42ff-4d4e-a05e-9a4e3fd45274'); --xyzies.authorization.reviews.mobile to Reviews Mobile Login

insert into [PermissionToPolicy] values ('96bf3985-0536-4bea-9bb1-8c71a86da6fc', 'b695018c-c264-4246-9e33-9dce90f338c2'); --xyzies.authorization.vsp.operator permissions to Vsp Operator Login Policy
insert into [PermissionToPolicy] values ('cace7e20-c5f9-4e98-984d-76fd8c35c4d5', '9ffce075-3299-4227-aae3-859f3c6e9eb6'); --authorization.vsp.mobile permissions to Vsp Mobile Login Policy

insert into [PolicyToRole] values ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'da671d01-1133-4cc9-94a6-b77587f21fad'); --TemplatesFull policy for operation admin
insert into [PolicyToRole] values ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'da671d01-1133-4cc9-94a6-b77587f21fad'); --ReviewsFull policy for operation admin
insert into [PolicyToRole] values ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'da671d01-1133-4cc9-94a6-b77587f21fad'); --ReviewsAdminLogin policy for operation admin

insert into [PolicyToRole] values ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'); --TemplatesFull policy for System admin
insert into [PolicyToRole] values ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'); --ReviewsFull policy for System admin
insert into [PolicyToRole] values ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'); --ReviewsAdminLogin policy for System admin

insert into [PolicyToRole] values ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'); --TemplatesFull policy for account admin
insert into [PolicyToRole] values ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'); --ReviewsFull policy for account admin
insert into [PolicyToRole] values ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'); --ReviewsAdminLogin policy for account admin

insert into [PolicyToRole] values ('415b2993-7e32-4859-8b59-2b527bcdeea1', '87a421a2-60e1-46c9-8e2e-679c1f5f3c8e'); --TemplatesFull policy for supportadmin
insert into [PolicyToRole] values ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', '87a421a2-60e1-46c9-8e2e-679c1f5f3c8e'); --ReviewsFull policy for support admin
insert into [PolicyToRole] values ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', '87a421a2-60e1-46c9-8e2e-679c1f5f3c8e'); --ReviewsAdminLogin policy for support admin

insert into [PolicyToRole] values ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'); --ReviewsAdminLogin policy for super admin
insert into [PolicyToRole] values ('9ffce075-3299-4227-aae3-859f3c6e9eb6', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'); --VspMobileLogin policy for super admin

insert into [PolicyToRole] values ('71f11476-42ff-4d4e-a05e-9a4e3fd45274', 'a89d3c96-5f4d-475f-8588-08e1523feffb'); --ReviewsMobileLogin policies for SalesRep

insert into [PolicyToRole] values ('b695018c-c264-4246-9e33-9dce90f338c2', '384c5edb-9db2-4cc5-bada-6b265a61ae6c'); --VspOperatorLogin policies for Operator