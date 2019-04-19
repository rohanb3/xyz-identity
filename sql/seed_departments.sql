IF NOT EXISTS (SELECT * FROM [Departments] 
            WHERE [Name] = 'DEFAULT 1')
INSERT INTO [Departments] ([Id], [Name])
VALUES (NEWID(), 'DEFAULT 1')

IF NOT EXISTS (SELECT * FROM [Departments] 
            WHERE [Name] = 'DEFAULT 2')
INSERT INTO [Departments] ([Id], [Name])
VALUES (NEWID(), 'DEFAULT 2')
