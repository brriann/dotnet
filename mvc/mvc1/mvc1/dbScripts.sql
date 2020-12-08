
--###
--### Migration: TruncateSkierName
--###

-- alter col Skier.Name from nvarchar(max) to nvarchar(20)

UPDATE s
SET s.[Name] = LEFT(s.[Name], 20)
FROM Skier s
WHERE LEN(s.[Name]) > 20


--###
--###
--###


--###
--###
--###