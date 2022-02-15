USE master
GO

CREATE LOGIN DvdLibraryApp WITH PASSWORD='testing123'
GO

USE DvdLibrary
GO
 
CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp
GO
  
GRANT EXECUTE ON dbo.DvdDelete TO DvdLibraryApp;  
GO

GRANT EXECUTE ON dbo.DvdInsert TO DvdLibraryApp;  
GO

GRANT EXECUTE ON dbo.DvdSelectAll TO DvdLibraryApp;  
GO

GRANT EXECUTE ON dbo.DvdSelectById TO DvdLibraryApp;  
GO

GRANT EXECUTE ON dbo.DvdUpdate TO DvdLibraryApp;  
GO

GRANT EXECUTE TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectAll TO DvdLibraryApp
GRANT EXECUTE ON DvdSelectById TO DvdLibraryApp
GRANT EXECUTE ON DvdInsert TO DvdLibraryApp
GRANT EXECUTE ON DvdUpdate TO DvdLibraryApp
GRANT EXECUTE ON DvdDelete TO DvdLibraryApp
GO