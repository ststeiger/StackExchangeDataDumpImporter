
--SELECT DISTINCT data_type FROM information_schema.columns 



-- http://wiki.ispirer.com/sqlways/sql-server/data-types/ntext
-- http://www.sqlines.com/postgresql/how-to/create_user_defined_type


-- uniqueidentifier ==> uuid
-- datetime ==> timestamp without time
-- bit ==> boolean
-- tinyint ==> smallint 
-- national character varying(MAX)  ==> text





CREATE TABLE Badges( Id int NOT NULL
,UserId int NOT NULL
,Name national character varying(50)  NOT NULL
,Date timestamp without time zone NOT NULL
,Class smallint NOT NULL
,TagBased boolean NOT NULL
);



CREATE TABLE CloseAsOffTopicReasonTypes( Id smallint NOT NULL
,IsUniversal boolean NOT NULL
,MarkdownMini national character varying(500)  NOT NULL
,CreationDate timestamp without time zone NOT NULL
,CreationModeratorId int 
,ApprovalDate timestamp without time zone 
,ApprovalModeratorId int 
,DeactivationDate timestamp without time zone 
,DeactivationModeratorId int 
);



CREATE TABLE CloseReasonTypes( Id smallint NOT NULL
,Name national character varying(200)  NOT NULL
,Description national character varying(500)  
);



CREATE TABLE Comments( Id int NOT NULL
,PostId int NOT NULL
,Score int NOT NULL
,Text national character varying(600)  NOT NULL
,CreationDate timestamp without time zone NOT NULL
,UserDisplayName national character varying(30)  
,UserId int 
);



CREATE TABLE FlagTypes( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
,Description national character varying(500)  NOT NULL
);



CREATE TABLE PendingFlags( Id int NOT NULL
,FlagTypeId smallint NOT NULL
,PostId int NOT NULL
,CreationDate date 
,CloseReasonTypeId smallint 
,CloseAsOffTopicReasonTypeId smallint 
,DuplicateOfQuestionId int 
,BelongsOnBaseHostAddress national character varying(100)  
);



CREATE TABLE PostFeedback( Id int NOT NULL
,PostId int NOT NULL
,IsAnonymous boolean 
,VoteTypeId smallint NOT NULL
,CreationDate timestamp without time zone NOT NULL
);



CREATE TABLE PostHistory( Id int NOT NULL
,PostHistoryTypeId smallint NOT NULL
,PostId int NOT NULL
,RevisionGUID uuid NOT NULL
,CreationDate timestamp without time zone NOT NULL
,UserId int 
,UserDisplayName national character varying(40)  
,Comment national character varying(400)  
,Text text  
);



CREATE TABLE PostHistoryTypes( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
);



CREATE TABLE PostLinks( Id int NOT NULL
,CreationDate timestamp without time zone NOT NULL
,PostId int NOT NULL
,RelatedPostId int NOT NULL
,LinkTypeId smallint NOT NULL
);



CREATE TABLE Posts( Id int NOT NULL
,PostTypeId smallint NOT NULL
,AcceptedAnswerId int 
,ParentId int 
,CreationDate timestamp without time zone NOT NULL
,DeletionDate timestamp without time zone 
,Score int 
,ViewCount int 
,Body text  
,OwnerUserId int 
,OwnerDisplayName national character varying(40)  
,LastEditorUserId int 
,LastEditorDisplayName national character varying(40)  
,LastEditDate timestamp without time zone 
,LastActivityDate timestamp without time zone 
,Title national character varying(250)  
,Tags national character varying(250)  
,AnswerCount int 
,CommentCount int 
,FavoriteCount int 
,ClosedDate timestamp without time zone 
,CommunityOwnedDate timestamp without time zone 
);



CREATE TABLE PostsWithDeleted( Id int NOT NULL
,PostTypeId smallint NOT NULL
,AcceptedAnswerId int 
,ParentId int 
,CreationDate timestamp without time zone NOT NULL
,DeletionDate timestamp without time zone 
,Score int 
,ViewCount int 
,Body text  
,OwnerUserId int 
,OwnerDisplayName national character varying(40)  
,LastEditorUserId int 
,LastEditorDisplayName national character varying(40)  
,LastEditDate timestamp without time zone 
,LastActivityDate timestamp without time zone 
,Title national character varying(250)  
,Tags national character varying(250)  
,AnswerCount int 
,CommentCount int 
,FavoriteCount int 
,ClosedDate timestamp without time zone 
,CommunityOwnedDate timestamp without time zone 
);



CREATE TABLE PostTags( PostId int NOT NULL
,TagId int NOT NULL
);



CREATE TABLE PostTypes( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
);



CREATE TABLE ReviewRejectionReasons( Id smallint NOT NULL
,Name national character varying(100)  NOT NULL
,Description national character varying(300)  NOT NULL
,PostTypeId smallint 
);



CREATE TABLE ReviewTaskResults( Id int NOT NULL
,ReviewTaskId int NOT NULL
,ReviewTaskResultTypeId smallint NOT NULL
,CreationDate date 
,RejectionReasonId smallint 
,Comment national character varying(150)  
);



CREATE TABLE ReviewTaskResultTypes( Id smallint NOT NULL
,Name national character varying(100)  NOT NULL
,Description national character varying(300)  NOT NULL
);



CREATE TABLE ReviewTasks( Id int NOT NULL
,ReviewTaskTypeId smallint NOT NULL
,CreationDate date 
,DeletionDate date 
,ReviewTaskStateId smallint NOT NULL
,PostId int NOT NULL
,SuggestedEditId int 
,CompletedByReviewTaskId int 
);



CREATE TABLE ReviewTaskStates( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
,Description national character varying(300)  NOT NULL
);



CREATE TABLE ReviewTaskTypes( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
,Description national character varying(300)  NOT NULL
);



CREATE TABLE SuggestedEdits( Id int NOT NULL
,PostId int NOT NULL
,CreationDate timestamp without time zone 
,ApprovalDate timestamp without time zone 
,RejectionDate timestamp without time zone 
,OwnerUserId int 
,Comment national character varying(800)  
,Text text  
,Title national character varying(250)  
,Tags national character varying(250)  
,RevisionGUID uuid 
);



CREATE TABLE SuggestedEditVotes( Id int NOT NULL
,SuggestedEditId int NOT NULL
,UserId int NOT NULL
,VoteTypeId smallint NOT NULL
,CreationDate timestamp without time zone NOT NULL
,TargetUserId int 
,TargetRepChange int NOT NULL
);



CREATE TABLE Tags( Id int NOT NULL
,TagName national character varying(35)  
,Count int NOT NULL
,ExcerptPostId int 
,WikiPostId int 
);



CREATE TABLE TagSynonyms( Id int NOT NULL
,SourceTagName national character varying(35)  
,TargetTagName national character varying(35)  
,CreationDate timestamp without time zone NOT NULL
,OwnerUserId int NOT NULL
,AutoRenameCount int NOT NULL
,LastAutoRename timestamp without time zone 
,Score int NOT NULL
,ApprovedByUserId int 
,ApprovalDate timestamp without time zone 
);



CREATE TABLE Users( Id int NOT NULL
,Reputation int NOT NULL
,CreationDate timestamp without time zone NOT NULL
,DisplayName national character varying(40)  
,LastAccessDate timestamp without time zone NOT NULL
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



CREATE TABLE Votes( Id int NOT NULL
,PostId int NOT NULL
,VoteTypeId smallint NOT NULL
,UserId int 
,CreationDate timestamp without time zone 
,BountyAmount int 
);



CREATE TABLE VoteTypes( Id smallint NOT NULL
,Name national character varying(50)  NOT NULL
);
