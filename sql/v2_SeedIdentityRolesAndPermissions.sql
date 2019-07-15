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
    ('870d6172-a3fa-4348-96be-8cb9a01edf54', 'xyzies.reconciliation.web.disputestatistic', 1),--13
    ('dbd354be-7588-4684-9ed2-f8bd96af332b', 'xyzies.reconciliation.web.teamstatistic', 1), --14
    ('5bf6ad33-6e29-460b-a871-66f5d56185ba', 'xyzies.reconciliation.web.disputedashboard', 1),--15
------------------------------------Order-------------------------------------------------------------
    ('3e2dd00d-7d25-4c43-9c87-fc7ee17604c7', 'xyzies.reconciliation.web.order.read', 1), --16 read orders WITHOUT column "Expected Comission"
    ('0a3787cf-2293-4dbe-8095-c9658f0eefb3', 'xyzies.reconciliation.web.order.read.systemadmin', 1), --17 read orders WITH column "Expected Comission"

------------------------------------Dispute-------------------------------------------------------------
    ('60647a09-f792-4cf3-88a2-41f7291f6181', 'xyzies.reconciliation.web.resubmissiontable.read', 1), -- 18
    ('7e228a8f-773f-4d74-9e50-a56c8fa6a729', 'xyzies.reconciliation.dispute.read', 1), --19
    ('00c8f930-b254-41fd-898d-b2e97a6918a3', 'xyzies.reconciliation.dispute.update', 1), -- 20
    ('c35e6e69-6476-41ba-99c8-ea512bc4cc5c', 'xyzies.reconciliation.dispute.create', 1), --21
    ('133eb68b-2a32-40e5-957c-79b9719bb2fc', 'xyzies.reconciliation.dispute.patch', 1), --22
    ('4dc5139b-dbbb-4b7d-a32d-78d5ab48268d', 'xyzies.reconciliation.dispute.patch.sam', 1), --23
    ('95e2d571-5fc5-4deb-8e3b-6c2e02d822c4', 'xyzies.reconciliation.dispute.delete', 1), --24
---------------------------------------------------------------------------------------

---------------------- Devices  -------------------------------------------------------
	('1df29ca9-f3b5-410d-b31e-8b85395fc1df', 'xyzies.devicemanagment.device.create', 1),--25
	('291aebb0-729f-4c51-abce-c141ddffe40d', 'xyzies.devicemanagment.device.update', 1),--26
	('c3ff0c81-0790-4d44-8307-92507bf98621', 'xyzies.devicemanagment.device.read', 1),--27
	('087f272d-ea01-4670-9e3e-2c6a5498f80c', 'xyzies.devicemanagment.device.delete', 1),--28

	('7f854cef-57a1-4fae-b8b7-be8b8713dfee', 'xyzies.devicemanagment.device.create.admin', 1),--29
	('14b656a6-2c23-4300-b9b8-7051b0745d84', 'xyzies.devicemanagment.device.update.admin', 1),--30
	('ce38932d-53f6-46e4-abe0-12cdd6bc6b4a', 'xyzies.devicemanagment.device.read.admin', 1),--31
	('8902b1ad-de90-4619-9c40-0486a0a8c32b', 'xyzies.devicemanagment.device.delete.admin', 1),--32
---------------------------------------------------------------------------------------

---------------------- Comments  -------------------------------------------------------
	('2c307cb5-0d8a-42f9-982b-7fbb3172c51d', 'xyzies.devicemanagment.comment.create', 1),--33
	('0c538b5f-84d0-4d1b-bba5-70d4c7969678', 'xyzies.devicemanagment.comment.read', 1),--34
---------------------------------------------------------------------------------------

---------------------- History  -------------------------------------------------------
	('a88cb33c-1a02-4511-9dba-546e3ed239d6', 'xyzies.devicemanagment.history.read', 1),--35
	('63dfac6d-3d56-4072-ac4b-ad628ef7dd99', 'xyzies.devicemanagment.history.read.admin', 1),--36
---------------------------------------------------------------------------------------

---------------------- Notification  -------------------------------------------------------
	('7cc53c19-0bf0-4cc9-ab3f-7702f459e43c', 'xyzies.notification.email.create', 1),--37
---------------------------------------------------------------------------------------

-------------------------------------Last-----------------------------------------------------------------------------------------------
    ('cace7e20-c5f9-4e98-984d-76fd8c35c4d6', 'xyzies.identity.user.read.all', 1),--38
	('cace7e20-c5f9-4e98-984d-76fd8c35c4d7', 'xyzies.identity.user.read.incompany', 1),--39
	('cace7e20-c5f9-4e98-984d-76fd8c35c4d8', 'xyzies.identity.user.read.myself', 1);--40
------------------------------------------------------------------------------------------------------------------------------------

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
    ('c096dfa8-2cd3-422d-afd4-c20970e0c01c', 'DisputeManager'), --8
    ('601e493f-61b3-4fac-89cb-a8d21e331abc', 'DisputeAdmin'), --9
    ('a9ffa03d-8a57-40ba-a75b-3b4a823b0b13', 'OutDisputeAdmin'), --10
    ('489b2156-6cb3-4d76-b5a4-c13776b82421', 'OrderManager'), --11
    ('f778d726-e016-47ec-b6ea-8d016937ae73', 'OrderAdmin'), --12
    ('bea8223a-dd34-477a-ae9b-ff6537a84d6f', 'DisputeSam'), --13
-----------------------------------------------------------------------
---------------------- Devices  -------------------------------------------------------
	('128cdc31-c597-4ca6-bdbc-55d66e31f698', 'DeviceBase'),--14
	('d064b092-dbb9-4622-88f8-32b9c44e5cec', 'DeviceAdmin'),--15
---------------------------------------------------------------------------------------
---------------------- Comments  -------------------------------------------------------
	('ea3431be-811d-449f-b51b-5deda83cdc8d', 'CommentBase'),--16
---------------------------------------------------------------------------------------
---------------------- History  -------------------------------------------------------
	('a104bad3-cdad-4722-9c16-2f735f58ffc0', 'HistoryBase'),--17
	('f1a2b40a-781e-416b-aeed-21961b01c86a', 'HistoryAdmin'),--18
---------------------------------------------------------------------------------------
---------------------- Notification  -------------------------------------------------------
	('9702e098-d66b-4d7b-a758-0f204a7b8321', 'NotificationBase'),--19
---------------------------------------------------------------------------------------

-------------------------------------Last-----------------------------------------------------------------------------------------------
	('415b2993-7e32-4859-8b59-2b527bcdeea2', 'ReadAllUsers'),--20
	('415b2993-7e32-4859-8b59-2b527bcdeea3', 'ReadUsersInCompany'),--21
	('415b2993-7e32-4859-8b59-2b527bcdeea4', 'ReadOnlyRequester');--22
------------------------------------------------------------------------------------------------------------------------------------

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
    ('af16483d-9c81-4443-84d6-a976e54fbfa9', 11, 'SAM', NULL, getdate(), 0),--11
    ('1727f429-580a-4d6b-b3fe-bce0ebbbd210', 12, 'Role #12', NULL, getdate(), 0),--12
    ('13e75147-0736-45db-bf34-d3b0aebdeafe', 13, 'Role #13', NULL, getdate(), 0),--13
    ('d47fd2ad-46ce-4173-8689-88573aa9ffac', 14, 'Operation Assistant', NULL, getdate(), 0),--14
    ('1e0b4f66-d15e-4366-994a-5bba45db1540', 15, 'Role #15', NULL, getdate(), 0),--15
    ('b2669957-2b49-460b-9074-3a68aa075c12', 16, 'Role #16', NULL, getdate(), 0),--16
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
--------------------------------------------------------------- Reconciliation ---------------------------------
    ('cfd0b910-4310-4d68-8796-1707307e26f5', '221315e1-b212-41f4-bf3a-ce6b5bbb9f7a'),--xyzies.authorization.reconciliation.web permissions to ReconciliationLogin
    ('870d6172-a3fa-4348-96be-8cb9a01edf54', '7b0ad5ee-429d-4577-ad70-7d3323069804'),--xyzies.reconciliation.web.disputestatistic permissions to ReconciliationWebAdmin
    ('dbd354be-7588-4684-9ed2-f8bd96af332b', '7b0ad5ee-429d-4577-ad70-7d3323069804'),--xyzies.reconciliation.web.teamstatistic permissions to ReconciliationWebAdmin
    ('5bf6ad33-6e29-460b-a871-66f5d56185ba', '7b0ad5ee-429d-4577-ad70-7d3323069804'),--xyzies.reconciliation.web.disputedashboard permissions to ReconciliationWebAdmin
-----------------------------Dispute-------------------------------------------------------------------------------   
    ('7e228a8f-773f-4d74-9e50-a56c8fa6a729', 'c096dfa8-2cd3-422d-afd4-c20970e0c01c'), --xyzies.reconciliation.dispute.read to DisputeManager

    ('00c8f930-b254-41fd-898d-b2e97a6918a3', '601e493f-61b3-4fac-89cb-a8d21e331abc'), --xyzies.reconciliation.dispute.update to DisputeAdmin
    ('60647a09-f792-4cf3-88a2-41f7291f6181', 'a9ffa03d-8a57-40ba-a75b-3b4a823b0b13'), --xyzies.reconciliation.web.resubmissiontable.read to OutDisputeAdmin
    ('c35e6e69-6476-41ba-99c8-ea512bc4cc5c', '601e493f-61b3-4fac-89cb-a8d21e331abc'), --'xyzies.reconciliation.dispute.create to DisputeAdmin
    ('133eb68b-2a32-40e5-957c-79b9719bb2fc', '601e493f-61b3-4fac-89cb-a8d21e331abc'), --xyzies.reconciliation.dispute.patch to DisputeAdmin
    ('4dc5139b-dbbb-4b7d-a32d-78d5ab48268d', 'bea8223a-dd34-477a-ae9b-ff6537a84d6f'), --xyzies.reconciliation.dispute.patch.sam to DisputeSam
    ('95e2d571-5fc5-4deb-8e3b-6c2e02d822c4', '601e493f-61b3-4fac-89cb-a8d21e331abc'), --xyzies.reconciliation.dispute.delete to DisputeAdmin
-----------------------------Order----------------------------------------------------------------------------------
    ('3e2dd00d-7d25-4c43-9c87-fc7ee17604c7', '489b2156-6cb3-4d76-b5a4-c13776b82421'), --xyzies.reconciliation.web.order.read to OrderManager

    ('0a3787cf-2293-4dbe-8095-c9658f0eefb3', 'f778d726-e016-47ec-b6ea-8d016937ae73'), --xyzies.reconciliation.web.order.read.systemadmin to OrderAdmin

-----------------------------------------------------------------------
---------------------- Devices  -------------------------------------------------------
	('1df29ca9-f3b5-410d-b31e-8b85395fc1df', '128cdc31-c597-4ca6-bdbc-55d66e31f698'), --xyzies.devicemanagment.create to DeviceBase
	('291aebb0-729f-4c51-abce-c141ddffe40d', '128cdc31-c597-4ca6-bdbc-55d66e31f698'), --xyzies.devicemanagment.update to DeviceBase
	('c3ff0c81-0790-4d44-8307-92507bf98621', '128cdc31-c597-4ca6-bdbc-55d66e31f698'), --xyzies.devicemanagment.read to DeviceBase
	('087f272d-ea01-4670-9e3e-2c6a5498f80c', '128cdc31-c597-4ca6-bdbc-55d66e31f698'), --xyzies.devicemanagment.delete to DeviceBase

	('7f854cef-57a1-4fae-b8b7-be8b8713dfee', 'd064b092-dbb9-4622-88f8-32b9c44e5cec'), --xyzies.devicemanagment.create.admin to DeviceAdmin
	('14b656a6-2c23-4300-b9b8-7051b0745d84', 'd064b092-dbb9-4622-88f8-32b9c44e5cec'), --xyzies.devicemanagment.update.admin to DeviceAdmin
	('ce38932d-53f6-46e4-abe0-12cdd6bc6b4a', 'd064b092-dbb9-4622-88f8-32b9c44e5cec'), --xyzies.devicemanagment.read.admin to DeviceAdmin
	('8902b1ad-de90-4619-9c40-0486a0a8c32b', 'd064b092-dbb9-4622-88f8-32b9c44e5cec'), --xyzies.devicemanagment.delete.admin to DeviceAdmin
---------------------------------------------------------------------------------------
---------------------- Comments  -------------------------------------------------------
	('2c307cb5-0d8a-42f9-982b-7fbb3172c51d', 'ea3431be-811d-449f-b51b-5deda83cdc8d'), --xyzies.devicemanagment.create to CommentBase
	('0c538b5f-84d0-4d1b-bba5-70d4c7969678', 'ea3431be-811d-449f-b51b-5deda83cdc8d'), --xyzies.devicemanagment.read to CommentBase
---------------------------------------------------------------------------------------
---------------------- History  -------------------------------------------------------
	('a88cb33c-1a02-4511-9dba-546e3ed239d6', 'a104bad3-cdad-4722-9c16-2f735f58ffc0'), --xyzies.devicemanagment.history.read to HistoryBase 
	('63dfac6d-3d56-4072-ac4b-ad628ef7dd99', 'f1a2b40a-781e-416b-aeed-21961b01c86a'), --xyzies.devicemanagment.history.read.admin to HistoryAdmin 
---------------------------------------------------------------------------------------
---------------------- Notification  -------------------------------------------------------
	('7cc53c19-0bf0-4cc9-ab3f-7702f459e43c', '9702e098-d66b-4d7b-a758-0f204a7b8321'), --xyzies.notification.email.create to NotificationBase
---------------------------------------------------------------------------------------

-------------------------------------Last-----------------------------------------------------------------------------------------------
	('cace7e20-c5f9-4e98-984d-76fd8c35c4d6', '415b2993-7e32-4859-8b59-2b527bcdeea2'), -- xyzies.identity.user.read.all to ReadAllUsers
	('cace7e20-c5f9-4e98-984d-76fd8c35c4d7', '415b2993-7e32-4859-8b59-2b527bcdeea3'), -- xyzies.identity.user.read.incompany to ReadUsersInCompany
	('cace7e20-c5f9-4e98-984d-76fd8c35c4d8', '415b2993-7e32-4859-8b59-2b527bcdeea4'); -- xyzies.identity.user.read.myself to ReadOnlyRequester
------------------------------------------------------------------------------------------------------------------------------------

insert into [PolicyToRole]
values

-------------------------------------Last-----------------------------------------------------------------------------------------------
	('415b2993-7e32-4859-8b59-2b527bcdeea3', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'), --ReadUsersInCompany for SuperAdmin
	('415b2993-7e32-4859-8b59-2b527bcdeea4', 'a89d3c96-5f4d-475f-8588-08e1523feffb'), --ReadOnlyRequester for SalesRep
	('415b2993-7e32-4859-8b59-2b527bcdeea3', 'c71b2170-98cf-4e69-8455-282fdbd21779'), --ReadUsersInCompany for Agent
	('415b2993-7e32-4859-8b59-2b527bcdeea3', '87a421a2-60e1-46c9-8e2e-679c1f5f3c8e'), --ReadUsersInCompany for SupportAdmin
	('415b2993-7e32-4859-8b59-2b527bcdeea2', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'), --ReadAllUsers for SystemAdmin
	
	('415b2993-7e32-4859-8b59-2b527bcdeea2', 'fafbb1b8-b039-4145-a923-f1c39d4fe603'), --ReadAllUsers for Manager
	('415b2993-7e32-4859-8b59-2b527bcdeea3', '37fdfbf6-3ee2-4827-b7be-cefe78213d92'), --ReadUsersInCompany for Supervisor
	('415b2993-7e32-4859-8b59-2b527bcdeea2', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'), --ReadAllUsers for AccountAdmin
	('415b2993-7e32-4859-8b59-2b527bcdeea2', 'da671d01-1133-4cc9-94a6-b77587f21fad'), --ReadAllUsers for OperationAdmin
	('415b2993-7e32-4859-8b59-2b527bcdeea2', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'), --ReadAllUsers for Operation Assistant
	
	('415b2993-7e32-4859-8b59-2b527bcdeea2', 'af16483d-9c81-4443-84d6-a976e54fbfa9'), --ReadAllUsers for SAM
	('415b2993-7e32-4859-8b59-2b527bcdeea2', 'a98dc9cb-7847-4858-8693-5fd11b24b58c'), --ReadAllUsers for Anonymous

------------------------------------------------------------------------------------------------------------------------------------
    ('c096dfa8-2cd3-422d-afd4-c20970e0c01c', 'da671d01-1133-4cc9-94a6-b77587f21fad'), --DisputeManager for operation admin
    ('c096dfa8-2cd3-422d-afd4-c20970e0c01c', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'), --DisputeManager for operation Assistant
    ('c096dfa8-2cd3-422d-afd4-c20970e0c01c', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'), --DisputeManager for account admin
    ('c096dfa8-2cd3-422d-afd4-c20970e0c01c', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'), --DisputeManager for system admin
    ('601e493f-61b3-4fac-89cb-a8d21e331abc', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'), --DisputeAdmin for system admin
    ('601e493f-61b3-4fac-89cb-a8d21e331abc', 'da671d01-1133-4cc9-94a6-b77587f21fad'), --DisputeAdmin for operation admin
    ('601e493f-61b3-4fac-89cb-a8d21e331abc', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'), --DisputeAdmin for operation assistant
    ('601e493f-61b3-4fac-89cb-a8d21e331abc', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'), --DisputeAdmin for account admin

    ('bea8223a-dd34-477a-ae9b-ff6537a84d6f', 'af16483d-9c81-4443-84d6-a976e54fbfa9'), --DisputeSam for SAM
    ('a9ffa03d-8a57-40ba-a75b-3b4a823b0b13', 'af16483d-9c81-4443-84d6-a976e54fbfa9'), --OutDisputeAdmin for SAM
	('a9ffa03d-8a57-40ba-a75b-3b4a823b0b13', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'), --OutDisputeAdmin for system admin

    ('489b2156-6cb3-4d76-b5a4-c13776b82421', 'da671d01-1133-4cc9-94a6-b77587f21fad'),  --OrderManager for operation admin
    ('489b2156-6cb3-4d76-b5a4-c13776b82421', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),  --OrderManager for operation assistant
    ('489b2156-6cb3-4d76-b5a4-c13776b82421', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),  --OrderManager for account admin
	('489b2156-6cb3-4d76-b5a4-c13776b82421', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),  --OrderManager for system admin

    ('f778d726-e016-47ec-b6ea-8d016937ae73', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),  --OrderAdmin for system admin

	('489b2156-6cb3-4d76-b5a4-c13776b82421', 'af16483d-9c81-4443-84d6-a976e54fbfa9'),  --OrderManager for SAM

---------------------------------------------------------------------------------------------------------------------------------------
    ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--TemplatesFull policy for operation admin
    ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--ReviewsFull policy for operation admin
    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--ReviewsAdminLogin policy for operation admin
    ('221315e1-b212-41f4-bf3a-ce6b5bbb9f7a', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--RecontiliationLogin policy for operation admin
    ('b695018c-c264-4246-9e33-9dce90f338c2', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--VspOperatorLogin policies for operation admin

    ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),--TemplatesFull policy for operation assistant
    ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),--ReviewsFull policy for operation assistant
    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),--ReviewsAdminLogin policy for operation assistant
    ('221315e1-b212-41f4-bf3a-ce6b5bbb9f7a', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),--RecontiliationLogin policy for operation assistant
    ('b695018c-c264-4246-9e33-9dce90f338c2', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),--VspOperatorLogin policies for operation assistant

    ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--TemplatesFull policy for System admin
    ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--ReviewsFull policy for System admin
    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--ReviewsAdminLogin policy for System admin
    ('221315e1-b212-41f4-bf3a-ce6b5bbb9f7a', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--RecontiliationLogin policy for System admin
    ('b695018c-c264-4246-9e33-9dce90f338c2', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--VspOperatorLogin policies for System admin

    ('415b2993-7e32-4859-8b59-2b527bcdeea1', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--TemplatesFull policy for account admin
    ('6f4ce9a2-1633-46b3-b7b6-5a93e5cbd3a2', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--ReviewsFull policy for account admin
    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--ReviewsAdminLogin policy for account admin
    ('221315e1-b212-41f4-bf3a-ce6b5bbb9f7a', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--RecontiliationLogin policy for account admin
    ('b695018c-c264-4246-9e33-9dce90f338c2', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--VspOperatorLogin policies for account admin

    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'),--ReviewsAdminLogin policy for super admin
    ('71f11476-42ff-4d4e-a05e-9a4e3fd45274', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'),--ReviewsMobileLogin policy for super admin
    ('9ffce075-3299-4227-aae3-859f3c6e9eb6', 'cb308b73-acf0-4f23-89b0-509b6bc0e7e6'),--VspMobileLogin policy for super admin

    ('71f11476-42ff-4d4e-a05e-9a4e3fd45274', 'a89d3c96-5f4d-475f-8588-08e1523feffb'),--ReviewsMobileLogin policies for SalesRep
    ('91d3b70e-3c7e-4faf-97c0-718809bf3a2a', 'a89d3c96-5f4d-475f-8588-08e1523feffb'),--ReviewsAdminLogin policies for SalesRep

    ('b695018c-c264-4246-9e33-9dce90f338c2', '87a421a2-60e1-46c9-8e2e-679c1f5f3c8e'),--VspOperatorLogin policies for supportadmin
    ('221315e1-b212-41f4-bf3a-ce6b5bbb9f7a', 'af16483d-9c81-4443-84d6-a976e54fbfa9'),--RecontiliationLogin policies for SAM
    ('b695018c-c264-4246-9e33-9dce90f338c2', '37fdfbf6-3ee2-4827-b7be-cefe78213d92'),--VspOperatorLogin policies for Superviser

----------------------- Devices  -------------------------------------------------------
	('d064b092-dbb9-4622-88f8-32b9c44e5cec', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'), --DeviceAdmin for AccountAdmin
	('d064b092-dbb9-4622-88f8-32b9c44e5cec', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--DeviceAdmin for OperationAdmin
	('d064b092-dbb9-4622-88f8-32b9c44e5cec', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),--DeviceAdmin for Operation Assistant
	('d064b092-dbb9-4622-88f8-32b9c44e5cec', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--DeviceAdmin for SystemAdmin

	('128cdc31-c597-4ca6-bdbc-55d66e31f698', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'), --DeviceBase for AccountAdmin
	('128cdc31-c597-4ca6-bdbc-55d66e31f698', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--DeviceBase for OperationAdmin
	('128cdc31-c597-4ca6-bdbc-55d66e31f698', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),--DeviceBase for Operation Assistant
	('128cdc31-c597-4ca6-bdbc-55d66e31f698', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--DeviceBase for SystemAdmin
	
	('128cdc31-c597-4ca6-bdbc-55d66e31f698', '37fdfbf6-3ee2-4827-b7be-cefe78213d92'),--DeviceBase for Supervisor

---------------------------------------------------------------------------------------
----------------------- Comments  -------------------------------------------------------
	('ea3431be-811d-449f-b51b-5deda83cdc8d', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--CommentBase for AccountAdmin
	('ea3431be-811d-449f-b51b-5deda83cdc8d', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--CommentBase for OperationAdmin
	('ea3431be-811d-449f-b51b-5deda83cdc8d', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),--CommentBase for Operation Assistant
	('ea3431be-811d-449f-b51b-5deda83cdc8d', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--CommentBase for SystemAdmin
	
	('ea3431be-811d-449f-b51b-5deda83cdc8d', '37fdfbf6-3ee2-4827-b7be-cefe78213d92'),--CommentBase for Supervisor

---------------------------------------------------------------------------------------
------------------------- History  -------------------------------------------------------
	('a104bad3-cdad-4722-9c16-2f735f58ffc0', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--HistoryBase for AccountAdmin
	('a104bad3-cdad-4722-9c16-2f735f58ffc0', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--HistoryBase for OperationAdmin
	('a104bad3-cdad-4722-9c16-2f735f58ffc0', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),--HistoryBase for Operation Assistant
	('a104bad3-cdad-4722-9c16-2f735f58ffc0', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--HistoryBase for SystemAdmin
	
	('f1a2b40a-781e-416b-aeed-21961b01c86a', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--HistoryAdmin for AccountAdmin
	('f1a2b40a-781e-416b-aeed-21961b01c86a', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--HistoryAdmin for OperationAdmin
	('f1a2b40a-781e-416b-aeed-21961b01c86a', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),--HistoryAdmin for Operation Assistant
	('f1a2b40a-781e-416b-aeed-21961b01c86a', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--HistoryAdmin for SystemAdmin

	('a104bad3-cdad-4722-9c16-2f735f58ffc0', '37fdfbf6-3ee2-4827-b7be-cefe78213d92'),--HistoryBase for Supervisor

---------------------------------------------------------------------------------------
----------------------- Notification  -------------------------------------------------------
	('9702e098-d66b-4d7b-a758-0f204a7b8321', 'ee9aa7f3-b3b5-4f64-a79c-ce192c576ad9'),--NotificationBase for AccountAdmin
	('9702e098-d66b-4d7b-a758-0f204a7b8321', 'da671d01-1133-4cc9-94a6-b77587f21fad'),--NotificationBase for OperationAdmin
	('9702e098-d66b-4d7b-a758-0f204a7b8321', 'd47fd2ad-46ce-4173-8689-88573aa9ffac'),--NotificationBase for Operation Assistant
	('9702e098-d66b-4d7b-a758-0f204a7b8321', 'a2285edf-44d0-4f2b-be30-4d6e49644da2'),--NotificationBase for SystemAdmin
	
	('9702e098-d66b-4d7b-a758-0f204a7b8321', '37fdfbf6-3ee2-4827-b7be-cefe78213d92');--NotificationBase for Supervisor
	
---------------------------------------------------------------------------------------