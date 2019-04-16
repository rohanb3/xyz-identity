USE [xyzies-identity]
GO
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

insert into [TWC_Role] values ('0578455d-6fcb-4c25-aa90-2144914b6635', 1, 'SuperAdmin', NULL, getdate(), 1); --0
insert into [TWC_Role] values ('cb308b73-acf0-4f23-89b0-509b6bc0e7e6', 2, 'RetailerAdmin', NULL,  getdate(), 1); --1
insert into [TWC_Role] values ('5459404d-ffb7-4efe-bcb9-8ea84942a575', 3, 'SalesRep', NULL,  getdate(), 1); --2
insert into [TWC_Role] values ('8c5f7cc1-1cba-44cf-bcc5-6755323c2725', 4, 'Operator', NULL,  getdate(), 1); --3

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

insert into [PolicyToRole] values ('415b2993-7e32-4859-8b59-2b527bcdeea1', '0578455d-6fcb-4c25-aa90-2144914b6635'); --TemplatesFull policy for super admin
insert into [PolicyToRole] values ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', '0578455d-6fcb-4c25-aa90-2144914b6635'); --ReviewsFull policy for super admin
insert into [PolicyToRole] values ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', '0578455d-6fcb-4c25-aa90-2144914b6635'); --ReviewsAdminLogin policy for super admin

insert into [PolicyToRole] values ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'); --ReviewsAdminLogin policy for retailer admin
insert into [PolicyToRole] values ('9ffce075-3299-4227-aae3-859f3c6e9eb6', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'); --VspMobileLogin policy for retailer admin

insert into [PolicyToRole] values ('71f11476-42ff-4d4e-a05e-9a4e3fd45274', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'); --ReviewsMobileLogin policies for SalesRep

insert into [PolicyToRole] values ('b695018c-c264-4246-9e33-9dce90f338c2', '8c5f7cc1-1cba-44cf-bcc5-6755323c2725'); --VspOperatorLogin policies for Operator