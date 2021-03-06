
-- SELECT DISTINCT data_type FROM information_schema.columns 


-- http://wiki.ispirer.com/sqlways/sql-server/data-types/ntext
-- http://www.sqlines.com/postgresql/how-to/create_user_defined_type


-- uniqueidentifier ==> uuid
-- datetime ==> datetime
-- bit ==> boolean
-- tinyint ==> smallint 
-- national character varying(MAX)  ==> text






CREATE TABLE IF NOT EXISTS Badges( Id int NOT NULL
,UserId int NOT NULL
,Name national character varying(50)  NOT NULL
,Date datetime NOT NULL
,Class smallint NOT NULL
,TagBased boolean NOT NULL
);


CREATE TABLE IF NOT EXISTS CloseAsOffTopicReasonTypes( Id smallint NOT NULL
,IsUniversal boolean NOT NULL
,MarkdownMini national character varying(500)  NOT NULL
,CreationDate datetime NOT NULL
,CreationModeratorId int 
,ApprovalDate datetime 
,ApprovalModeratorId int 
,DeactivationDate datetime 
,DeactivationModeratorId int 
);



CREATE TABLE IF NOT EXISTS CloseReasonTypes( Id smallint NOT NULL
,Name national character varying(200)  NOT NULL
,Description national character varying(500)  
);



CREATE TABLE IF NOT EXISTS Comments( Id int NOT NULL
,PostId int NOT NULL
,Score int NOT NULL
,Text national character varying(600)  NOT NULL
,CreationDate datetime NOT NULL
,UserDisplayName national character varying(30)  
,UserId int 
);



CREATE TABLE IF NOT EXISTS FlagTypes( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
,Description national character varying(500)  NOT NULL
);



CREATE TABLE IF NOT EXISTS PendingFlags( Id int NOT NULL
,FlagTypeId smallint NOT NULL
,PostId int NOT NULL
,CreationDate date 
,CloseReasonTypeId smallint 
,CloseAsOffTopicReasonTypeId smallint 
,DuplicateOfQuestionId int 
,BelongsOnBaseHostAddress national character varying(100)  
);



CREATE TABLE IF NOT EXISTS PostFeedback( Id int NOT NULL
,PostId int NOT NULL
,IsAnonymous boolean 
,VoteTypeId smallint NOT NULL
,CreationDate datetime NOT NULL
);



CREATE TABLE IF NOT EXISTS PostHistory( Id int NOT NULL
,PostHistoryTypeId smallint NOT NULL
,PostId int NOT NULL
,RevisionGUID character varying(36) NOT NULL
,CreationDate datetime NOT NULL
,UserId int 
,UserDisplayName national character varying(40)  
,Comment national character varying(400)  
,Text text  
);



CREATE TABLE IF NOT EXISTS PostHistoryTypes( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
);



CREATE TABLE IF NOT EXISTS PostLinks( Id int NOT NULL
,CreationDate datetime NOT NULL
,PostId int NOT NULL
,RelatedPostId int NOT NULL
,LinkTypeId smallint NOT NULL
);



CREATE TABLE IF NOT EXISTS Posts( Id int NOT NULL
,PostTypeId smallint NOT NULL
,AcceptedAnswerId int 
,ParentId int 
,CreationDate datetime NOT NULL
,DeletionDate datetime 
,Score int 
,ViewCount int 
,Body text  
,OwnerUserId int 
,OwnerDisplayName national character varying(40)  
,LastEditorUserId int 
,LastEditorDisplayName national character varying(40)  
,LastEditDate datetime 
,LastActivityDate datetime 
,Title national character varying(250)  
,Tags national character varying(250)  
,AnswerCount int 
,CommentCount int 
,FavoriteCount int 
,ClosedDate datetime 
,CommunityOwnedDate datetime 
);



CREATE TABLE IF NOT EXISTS PostsWithDeleted( Id int NOT NULL
,PostTypeId smallint NOT NULL
,AcceptedAnswerId int 
,ParentId int 
,CreationDate datetime NOT NULL
,DeletionDate datetime 
,Score int 
,ViewCount int 
,Body text  
,OwnerUserId int 
,OwnerDisplayName national character varying(40)  
,LastEditorUserId int 
,LastEditorDisplayName national character varying(40)  
,LastEditDate datetime 
,LastActivityDate datetime 
,Title national character varying(250)  
,Tags national character varying(250)  
,AnswerCount int 
,CommentCount int 
,FavoriteCount int 
,ClosedDate datetime 
,CommunityOwnedDate datetime 
);



CREATE TABLE IF NOT EXISTS PostTags( PostId int NOT NULL
,TagId int NOT NULL
);



CREATE TABLE IF NOT EXISTS PostTypes( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
);



CREATE TABLE IF NOT EXISTS ReviewRejectionReasons( Id smallint NOT NULL
,Name national character varying(100)  NOT NULL
,Description national character varying(300)  NOT NULL
,PostTypeId smallint 
);



CREATE TABLE IF NOT EXISTS ReviewTaskResults( Id int NOT NULL
,ReviewTaskId int NOT NULL
,ReviewTaskResultTypeId smallint NOT NULL
,CreationDate date 
,RejectionReasonId smallint 
,Comment national character varying(150)  
);



CREATE TABLE IF NOT EXISTS ReviewTaskResultTypes( Id smallint NOT NULL
,Name national character varying(100)  NOT NULL
,Description national character varying(300)  NOT NULL
);



CREATE TABLE IF NOT EXISTS ReviewTasks( Id int NOT NULL
,ReviewTaskTypeId smallint NOT NULL
,CreationDate date 
,DeletionDate date 
,ReviewTaskStateId smallint NOT NULL
,PostId int NOT NULL
,SuggestedEditId int 
,CompletedByReviewTaskId int 
);



CREATE TABLE IF NOT EXISTS ReviewTaskStates( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
,Description national character varying(300)  NOT NULL
);



CREATE TABLE IF NOT EXISTS ReviewTaskTypes( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
,Description national character varying(300)  NOT NULL
);



CREATE TABLE IF NOT EXISTS SuggestedEdits( Id int NOT NULL
,PostId int NOT NULL
,CreationDate datetime 
,ApprovalDate datetime 
,RejectionDate datetime 
,OwnerUserId int 
,Comment national character varying(800)  
,Text text  
,Title national character varying(250)  
,Tags national character varying(250)  
,RevisionGUID character varying(36)  
);




CREATE TABLE IF NOT EXISTS SuggestedEditVotes( Id int NOT NULL
,SuggestedEditId int NOT NULL
,UserId int NOT NULL
,VoteTypeId smallint NOT NULL
,CreationDate datetime NOT NULL
,TargetUserId int 
,TargetRepChange int NOT NULL
);



CREATE TABLE IF NOT EXISTS Tags( Id int NOT NULL
,TagName national character varying(35)  
,Count int NOT NULL
,ExcerptPostId int 
,WikiPostId int 
);



CREATE TABLE IF NOT EXISTS TagSynonyms( Id int NOT NULL
,SourceTagName national character varying(35)  
,TargetTagName national character varying(35)  
,CreationDate datetime NOT NULL
,OwnerUserId int NOT NULL
,AutoRenameCount int NOT NULL
,LastAutoRename datetime 
,Score int NOT NULL
,ApprovedByUserId int 
,ApprovalDate datetime 
);



CREATE TABLE IF NOT EXISTS Users( Id int NOT NULL
,Reputation int NOT NULL
,CreationDate datetime NOT NULL
,DisplayName national character varying(40)  
,LastAccessDate datetime NOT NULL
,WebsiteUrl national character varying(200)  
,Location national character varying(100)  
,AboutMe text  
,Views int NOT NULL
,UpVotes int NOT NULL
,DownVotes int NOT NULL
,ProfileImageUrl national character varying(200)  
,EmailHash character varying(32)  
,Age int 
,AccountId int 
);



CREATE TABLE IF NOT EXISTS Votes( Id int NOT NULL
,PostId int NOT NULL
,VoteTypeId smallint NOT NULL
,UserId int 
,CreationDate datetime 
,BountyAmount int 
);



CREATE TABLE IF NOT EXISTS VoteTypes( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
);
