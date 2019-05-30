delete from [Permissions];
delete from [Policies];
delete from [TWC_Role];

insert into [Permissions]
values
    ('814615e0-7785-4396-9f0d-b3715a135b9b', 'xyzies.reviews.templates.read', 1),--0
    ('40ea2f94-1214-4c2c-aa8e-6db0175e798b', 'xyzies.reviews.templates.write', 1),--1
    ('74338e7a-b8bd-4c22-ab27-22c31a41884d', 'xyzies.reviews.templates.update', 1),--2
    ('f617f927-f190-48d1-ba9c-6edcaea30732', 'xyzies.reviews.templates.delete', 1),--3
    ('e2365aef-552e-4a5b-b0e2-6eee5a5a444e', 'xyzies.reviews.reviews.read', 1),--4
    ('2e19850a-1e89-49b4-9a18-60f02a7639f1', 'xyzies.reviews.reviews.write', 1),--5
    ('418b0827-0a18-4f24-a9ae-85bd473d5abc', 'xyzies.reviews.reviews.update', 1),--6
    ('66dfa9f7-8c86-48e0-9ec5-7e382729985c', 'xyzies.reviews.reviews.delete', 1),--7
    ('63e781cc-7637-4d07-bed0-5629d5da92ec', 'xyzies.authorization.reviews.admin', 1),--8
    ('3a8a3a6c-4b9f-4425-81f1-476cddd436da', 'xyzies.authorization.reviews.mobile', 1),--9
    ('96bf3985-0536-4bea-9bb1-8c71a86da6fc', 'xyzies.authorization.vsp.web', 1),--10
    ('cace7e20-c5f9-4e98-984d-76fd8c35c4d5', 'xyzies.authorization.vsp.mobile', 1),--11
---------------------- Reconciliation ---------------------------------
    ('cfd0b910-4310-4d68-8796-1707307e26f5', 'xyzies.authorization.reconciliation.web', 1),--12
    ('3fba9eca-ff52-4040-b26d-4f31d4f8b5ea', 'xyzies.reconciliation.web.orderlist', 1),--13
    ('c594bb9e-de39-4917-a3d3-b9905b29451c', 'xyzies.reconciliation.web.disputelist', 1),--14
    ('870d6172-a3fa-4348-96be-8cb9a01edf54', 'xyzies.reconciliation.web.disputestatistic', 1),--15
    ('dbd354be-7588-4684-9ed2-f8bd96af332b', 'xyzies.reconciliation.web.teamstatistic', 1), --16
    ('5bf6ad33-6e29-460b-a871-66f5d56185ba', 'xyzies.reconciliation.web.disputedashboard', 1),--17
    ('c25d7b14-5718-416d-811c-14b92a19965a', 'xyzies.reconciliation.web.ressubmissiontable', 1),--18
---------------------------------------------------------------------------------------
---------------------- Devices  -------------------------------------------------------
	('1df29ca9-f3b5-410d-b31e-8b85395fc1df', 'xyzies.devicemanagment.device.create', 1),--19
	('291aebb0-729f-4c51-abce-c141ddffe40d', 'xyzies.devicemanagment.device.update', 1),--20
	('c3ff0c81-0790-4d44-8307-92507bf98621', 'xyzies.devicemanagment.device.read', 1),--21
	('087f272d-ea01-4670-9e3e-2c6a5498f80c', 'xyzies.devicemanagment.device.delete', 1),--22

	('7f854cef-57a1-4fae-b8b7-be8b8713dfee', 'xyzies.devicemanagment.device.create.admin', 1),--23
	('14b656a6-2c23-4300-b9b8-7051b0745d84', 'xyzies.devicemanagment.device.update.admin', 1),--24
	('ce38932d-53f6-46e4-abe0-12cdd6bc6b4a', 'xyzies.devicemanagment.device.read.admin', 1),--25
	('8902b1ad-de90-4619-9c40-0486a0a8c32b', 'xyzies.devicemanagment.device.delete.admin', 1);--26
---------------------------------------------------------------------------------------


insert into [Policies]
values
    ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'TemplatesFull'),--0
    ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'ReviewsFull'),--1
    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'ReviewsAdminLogin'),--2
    ('71f11476-42ff-4d4e-a05e-9a4e3fd45274', 'ReviewsMobileLogin'),--3
    ('b695018c-c264-4246-9e33-9dce90f338c2', 'VspSupportAdminLogin'),--4
    ('9ffce075-3299-4227-aae3-859f3c6e9eb6', 'VspMobileLogin'),--5
---------------------- Reconciliation ---------------------------------
    ('221315e1-b212-41f4-bf3a-ce6b5bbb9f7a', 'ReconciliationLogin'),--6
    ('7b0ad5ee-429d-4577-ad70-7d3323069804', 'ReconciliationWebAdmin'),--7
    ('cda0444f-2414-4498-8d01-0422f1aa08c2', 'ReconciliationWebManager'),--8
-----------------------------------------------------------------------
---------------------- Devices  -------------------------------------------------------
	('128cdc31-c597-4ca6-bdbc-55d66e31f698', 'DeviceBase'),--9
	('d064b092-dbb9-4622-88f8-32b9c44e5cec', 'DeviceAdmin');--10
---------------------------------------------------------------------------------------

insert into [TWC_Role]
values
    ('cb308b73-acf0-4f23-89b0-509b6bc0e7e6', 1, 'SuperAdmin', NULL, getdate(), 0),--1
    ('a89d3c96-5f4d-475f-8588-08e1523feffb', 2, 'SalesRep', NULL, getdate(), 0),--2
    ('c71b2170-98cf-4e69-8455-282fdbd21779', 3, 'Agent', NULL, getdate(), 0),--3
    ('87a421a2-60e1-46c9-8e2e-679c1f5f3c8e', 4, 'SupportAdmin', NULL, getdate(), 0),--4
    ('a2285edf-44d0-4f2b-be30-4d6e49644da2', 5, 'SystemAdmin', NULL, getdate(), 0),--5
    ('fafbb1b8-b039-4145-a923-f1c39d4fe603', 6, 'Manager', NULL, getdate(), 0),--6
    ('37fdfbf6-3ee2-4827-b7be-cefe78213d92', 7, 'Supervisor', NULL, getdate(), 0),--7
    ('ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9', 8, 'AccountAdmin', NULL, getdate(), 0),--8
    ('da671d01-1133-4cc9-94a6-b77587f21fad', 9, 'OperationAdmin', NULL, getdate(), 0),--9
    ('4d6de41d-bfdb-4a68-8988-017c2a4ce2af', 10, 'Role #10', NULL, getdate(), 0),--10
    ('af16483d-9c81-4443-84d6-a976e54fbfa9', 11, 'Role #11', NULL, getdate(), 0),--11
    ('1727f429-580a-4d6b-b3fe-bce0ebbbd210', 12, 'Role #12', NULL, getdate(), 0),--12
    ('13e75147-0736-45db-bf34-d3b0aebdeafe', 13, 'Role #13', NULL, getdate(), 0),--13
    ('d47fd2ad-46ce-4173-8689-88573aa9ffac', 14, 'Role #14', NULL, getdate(), 0),--14
    ('1e0b4f66-d15e-4366-994a-5bba45db1540', 15, 'Role #15', NULL, getdate(), 0),--15
    ('b2669957-2b49-460b-9074-3a68aa075c12', 16, 'Role #16', NULL, getdate(), 0),--16
    ('92b1d474-8764-4363-bf7c-05d8f0520bce', 18, 'SAM', NULL, getdate(), 0),--18
    ('a98dc9cb-7847-4858-8693-5fd11b24b58c', 0, 'Anonymous', NULL, getdate(), 0);--0

insert into [PermissionToPolicy]
values
    ('814615e0-7785-4396-9f0d-b3715a135b9b', '415b2993-7e32-4859-8b59-2b527bcdeea1'),--templates permissions to Templates Full Policy
    ('40ea2f94-1214-4c2c-aa8e-6db0175e798b', '415b2993-7e32-4859-8b59-2b527bcdeea1'),--templates permissions to Templates Full Policy
    ('74338e7a-b8bd-4c22-ab27-22c31a41884d', '415b2993-7e32-4859-8b59-2b527bcdeea1'),--templates permissions to Templates Full Policy
    ('f617f927-f190-48d1-ba9c-6edcaea30732', '415b2993-7e32-4859-8b59-2b527bcdeea1'),--templates permissions to Templates Full Policy
    ('e2365aef-552e-4a5b-b0e2-6eee5a5a444e', '6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2'),--reviews permissions to Reviews Full Policy
    ('2e19850a-1e89-49b4-9a18-60f02a7639f1', '6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2'),--reviews permissions to Reviews Full Policy
    ('418b0827-0a18-4f24-a9ae-85bd473d5abc', '6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2'),--reviews permissions to Reviews Full Policy
    ('66dfa9f7-8c86-48e0-9ec5-7e382729985c', '6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2'),--reviews permissions to Reviews Full Policy
    ('63e781cc-7637-4d07-bed0-5629d5da92ec', '91d3b70e-3c7e-4faf-97c0-718809bf3a2a'),--authorization.reviews.admin permissions to Reviews Admin Login
    ('3a8a3a6c-4b9f-4425-81f1-476cddd436da', '71f11476-42ff-4d4e-a05e-9a4e3fd45274'),--xyzies.authorization.reviews.mobile to Reviews Mobile Login
    ('96bf3985-0536-4bea-9bb1-8c71a86da6fc', 'b695018c-c264-4246-9e33-9dce90f338c2'),--xyzies.authorization.vsp.web permissions to Vsp Support Admin Login Policy
    ('cace7e20-c5f9-4e98-984d-76fd8c35c4d5', '9ffce075-3299-4227-aae3-859f3c6e9eb6'),--authorization.vsp.mobile permissions to Vsp Mobile Login Policy
    ---------------------- Reconciliation ---------------------------------
    ('cfd0b910-4310-4d68-8796-1707307e26f5', '221315e1-b212-41f4-bf3a-ce6b5bbb9f7a'),--xyzies.authorization.reconciliation.web permissions to ReconciliationLogin
    ('3fba9eca-ff52-4040-b26d-4f31d4f8b5ea', '7b0ad5ee-429d-4577-ad70-7d3323069804'),--xyzies.reconciliation.web.orderlist permissions to ReconciliationWebAdmin
    ('c594bb9e-de39-4917-a3d3-b9905b29451c', '7b0ad5ee-429d-4577-ad70-7d3323069804'),--xyzies.reconciliation.web.disputelist permissions to ReconciliationWebAdmin
    ('870d6172-a3fa-4348-96be-8cb9a01edf54', '7b0ad5ee-429d-4577-ad70-7d3323069804'),--xyzies.reconciliation.web.disputestatistic permissions to ReconciliationWebAdmin
    ('dbd354be-7588-4684-9ed2-f8bd96af332b', '7b0ad5ee-429d-4577-ad70-7d3323069804'),--xyzies.reconciliation.web.teamstatistic permissions to ReconciliationWebAdmin
    ('5bf6ad33-6e29-460b-a871-66f5d56185ba', '7b0ad5ee-429d-4577-ad70-7d3323069804'),--xyzies.reconciliation.web.disputedashboard permissions to ReconciliationWebAdmin
    ('c25d7b14-5718-416d-811c-14b92a19965a', 'cda0444f-2414-4498-8d01-0422f1aa08c2'),--xyzies.reconciliation.web.ressubmissiontable permissions to ReconciliationWebManager
    ('3fba9eca-ff52-4040-b26d-4f31d4f8b5ea', 'cda0444f-2414-4498-8d01-0422f1aa08c2'),--xyzies.reconciliation.web.orderlist permissions to ReconciliationWebManager
-----------------------------------------------------------------------
---------------------- Devices  -------------------------------------------------------
	('1df29ca9-f3b5-410d-b31e-8b85395fc1df', '128cdc31-c597-4ca6-bdbc-55d66e31f698'), --xyzies.devicemanagment.create to DeviceBase
	('291aebb0-729f-4c51-abce-c141ddffe40d', '128cdc31-c597-4ca6-bdbc-55d66e31f698'), --xyzies.devicemanagment.update to DeviceBase
	('c3ff0c81-0790-4d44-8307-92507bf98621', '128cdc31-c597-4ca6-bdbc-55d66e31f698'), --xyzies.devicemanagment.read to DeviceBase
	('087f272d-ea01-4670-9e3e-2c6a5498f80c', '128cdc31-c597-4ca6-bdbc-55d66e31f698'), --xyzies.devicemanagment.delete to DeviceBase

	('7f854cef-57a1-4fae-b8b7-be8b8713dfee', 'd064b092-dbb9-4622-88f8-32b9c44e5cec'), --xyzies.devicemanagment.create.admin to DeviceAdmin
	('14b656a6-2c23-4300-b9b8-7051b0745d84', 'd064b092-dbb9-4622-88f8-32b9c44e5cec'), --xyzies.devicemanagment.update.admin to DeviceAdmin
	('ce38932d-53f6-46e4-abe0-12cdd6bc6b4a', 'd064b092-dbb9-4622-88f8-32b9c44e5cec'), --xyzies.devicemanagment.read.admin to DeviceAdmin
	('8902b1ad-de90-4619-9c40-0486a0a8c32b', 'd064b092-dbb9-4622-88f8-32b9c44e5cec'); --xyzies.devicemanagment.delete.admin to DeviceAdmin
---------------------------------------------------------------------------------------


insert into [PolicyToRole]
values
    ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--TemplatesFull policy for operation admin
    ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--ReviewsFull policy for operation admin
    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--ReviewsAdminLogin policy for operation admin
    ('221315e1-b212-41f4-bf3a-ce6b5bbb9f7a', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--RecontiliationLogin policy for operation admin
    ('7b0ad5ee-429d-4577-ad70-7d3323069804', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--RecontiliationWebAdmin policy for operation admin
    ('b695018c-c264-4246-9e33-9dce90f338c2', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--VspOperatorLogin policies for operation admin

    ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--TemplatesFull policy for System admin
    ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--ReviewsFull policy for System admin
    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--ReviewsAdminLogin policy for System admin
    ('221315e1-b212-41f4-bf3a-ce6b5bbb9f7a', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--RecontiliationLogin policy for System admin
    ('7b0ad5ee-429d-4577-ad70-7d3323069804', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--RecontiliationWebAdmin policy for System admin
    ('b695018c-c264-4246-9e33-9dce90f338c2', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--VspOperatorLogin policies for System admin

    ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--TemplatesFull policy for account admin
    ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--ReviewsFull policy for account admin
    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--ReviewsAdminLogin policy for account admin
    ('221315e1-b212-41f4-bf3a-ce6b5bbb9f7a', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--RecontiliationLogin policy for account admin
    ('7b0ad5ee-429d-4577-ad70-7d3323069804', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--RecontiliationWebAdmin policy for account admin
    ('b695018c-c264-4246-9e33-9dce90f338c2', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--VspOperatorLogin policies for account admin

    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'),--ReviewsAdminLogin policy for super admin
    ('71f11476-42ff-4d4e-a05e-9a4e3fd45274', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'),--ReviewsMobileLogin policy for super admin
    ('9ffce075-3299-4227-aae3-859f3c6e9eb6', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'),--VspMobileLogin policy for super admin

    ('71f11476-42ff-4d4e-a05e-9a4e3fd45274', 'a89d3c96-5f4d-475f-8588-08e1523feffb'),--ReviewsMobileLogin policies for SalesRep
    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'a89d3c96-5f4d-475f-8588-08e1523feffb'),--ReviewsAdminLogin policies for SalesRep

    ('b695018c-c264-4246-9e33-9dce90f338c2', '87a421a2-60e1-46c9-8e2e-679c1f5f3c8e'),--VspOperatorLogin policies for supportadmin
    ('221315e1-b212-41f4-bf3a-ce6b5bbb9f7a', '92b1d474-8764-4363-bf7c-05d8f0520bce'),--RecontiliationLogin policies for SAM
    ('b695018c-c264-4246-9e33-9dce90f338c2', '37fdfbf6-3ee2-4827-b7be-cefe78213d92'),--VspOperatorLogin policies for Superviser

    ('cda0444f-2414-4498-8d01-0422f1aa08c2', '92b1d474-8764-4363-bf7c-05d8f0520bce'),--RecontiliationWebManager policies for SAM

	---------------------- Devices  -------------------------------------------------------
	('d064b092-dbb9-4622-88f8-32b9c44e5cec', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'), --DeviceAdmin for AccountAdmin
	('d064b092-dbb9-4622-88f8-32b9c44e5cec', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--DeviceAdmin for OperationAdmin
	('d064b092-dbb9-4622-88f8-32b9c44e5cec', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--DeviceAdmin for SystemAdmin

	('128cdc31-c597-4ca6-bdbc-55d66e31f698', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'), --DeviceBase for AccountAdmin
	('128cdc31-c597-4ca6-bdbc-55d66e31f698', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--DeviceBase for OperationAdmin
	('128cdc31-c597-4ca6-bdbc-55d66e31f698', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--DeviceBase for SystemAdmin
	
	('128cdc31-c597-4ca6-bdbc-55d66e31f698', '37fdfbf6-3ee2-4827-b7be-cefe78213d92');--DeviceBase for Supervisor

---------------------------------------------------------------------------------------