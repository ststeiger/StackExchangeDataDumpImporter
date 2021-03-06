
SELECT 
--,N'ALTER TABLE distributors ADD PRIMARY KEY (dist_id);' 
--,N'ALTER TABLE distributors ADD CONSTRAINT PK_distributors PRIMARY KEY NONCLUSTERED (pmid, persionId);'
--,N'ALTER TABLE distributors ADD CONSTRAINT PK_distributors PRIMARY KEY NONCLUSTERED (pmid, persionId);'
--,N'ALTER TABLE distributors ADD CONSTRAINT PK_distributors PRIMARY KEY (pmid);'
	N'ALTER TABLE ' + table_name + ' ADD CONSTRAINT PK_' + table_name + ' PRIMARY KEY ( ' + COLUMN_NAME + ' );' AS sql 
	,* 
FROM information_schema.columns 
WHERE ORDINAL_POSITION = 1 
AND TABLE_SCHEMA NOT IN('information_schema', 'pg_catalog') 
ORDER BY TABLE_NAME 
