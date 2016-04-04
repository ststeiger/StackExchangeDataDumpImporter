
CREATE TABLE Badges( Id int NOT NULL
,UserId int NOT NULL
,Name national character varying(50)  NOT NULL
,Date datetime NOT NULL
,Class tinyint NOT NULL
,TagBased bit NOT NULL
);



CREATE TABLE CloseAsOffTopicReasonTypes( Id smallint NOT NULL
,IsUniversal bit NOT NULL
,MarkdownMini national character varying(500)  NOT NULL
,CreationDate datetime NOT NULL
,CreationModeratorId int 
,ApprovalDate datetime 
,ApprovalModeratorId int 
,DeactivationDate datetime 
,DeactivationModeratorId int 
);



CREATE TABLE CloseReasonTypes( Id tinyint NOT NULL
,Name national character varying(200)  NOT NULL
,Description national character varying(500)  
);



CREATE TABLE Comments( Id int NOT NULL
,PostId int NOT NULL
,Score int NOT NULL
,Text national character varying(600)  NOT NULL
,CreationDate datetime NOT NULL
,UserDisplayName national character varying(30)  
,UserId int 
);



CREATE TABLE FlagTypes( Id tinyint NOT NULL
,Name national character varying(50)  NOT NULL
,Description national character varying(500)  NOT NULL
);



CREATE TABLE PendingFlags( Id int NOT NULL
,FlagTypeId tinyint NOT NULL
,PostId int NOT NULL
,CreationDate date 
,CloseReasonTypeId tinyint 
,CloseAsOffTopicReasonTypeId smallint 
,DuplicateOfQuestionId int 
,BelongsOnBaseHostAddress national character varying(100)  
);



CREATE TABLE PostFeedback( Id int NOT NULL
,PostId int NOT NULL
,IsAnonymous bit 
,VoteTypeId tinyint NOT NULL
,CreationDate datetime NOT NULL
);



CREATE TABLE PostHistory( Id int NOT NULL
,PostHistoryTypeId tinyint NOT NULL
,PostId int NOT NULL
,RevisionGUID uniqueidentifier NOT NULL
,CreationDate datetime NOT NULL
,UserId int 
,UserDisplayName national character varying(40)  
,Comment national character varying(400)  
,Text national character varying(MAX)  
);



CREATE TABLE PostHistoryTypes( Id tinyint NOT NULL
,Name national character varying(50)  NOT NULL
);



CREATE TABLE PostLinks( Id int NOT NULL
,CreationDate datetime NOT NULL
,PostId int NOT NULL
,RelatedPostId int NOT NULL
,LinkTypeId tinyint NOT NULL
);



CREATE TABLE Posts( Id int NOT NULL
,PostTypeId tinyint NOT NULL
,AcceptedAnswerId int 
,ParentId int 
,CreationDate datetime NOT NULL
,DeletionDate datetime 
,Score int 
,ViewCount int 
,Body national character varying(MAX)  
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



CREATE TABLE PostsWithDeleted( Id int NOT NULL
,PostTypeId tinyint NOT NULL
,AcceptedAnswerId int 
,ParentId int 
,CreationDate datetime NOT NULL
,DeletionDate datetime 
,Score int 
,ViewCount int 
,Body national character varying(MAX)  
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



CREATE TABLE PostTags( PostId int NOT NULL
,TagId int NOT NULL
);



CREATE TABLE PostTypes( Id tinyint NOT NULL
,Name national character varying(50)  NOT NULL
);



CREATE TABLE ReviewRejectionReasons( Id tinyint NOT NULL
,Name national character varying(100)  NOT NULL
,Description national character varying(300)  NOT NULL
,PostTypeId tinyint 
);



CREATE TABLE ReviewTaskResults( Id int NOT NULL
,ReviewTaskId int NOT NULL
,ReviewTaskResultTypeId tinyint NOT NULL
,CreationDate date 
,RejectionReasonId tinyint 
,Comment national character varying(150)  
);



CREATE TABLE ReviewTaskResultTypes( Id tinyint NOT NULL
,Name national character varying(100)  NOT NULL
,Description national character varying(300)  NOT NULL
);



CREATE TABLE ReviewTasks( Id int NOT NULL
,ReviewTaskTypeId tinyint NOT NULL
,CreationDate date 
,DeletionDate date 
,ReviewTaskStateId tinyint NOT NULL
,PostId int NOT NULL
,SuggestedEditId int 
,CompletedByReviewTaskId int 
);



CREATE TABLE ReviewTaskStates( Id tinyint NOT NULL
,Name national character varying(50)  NOT NULL
,Description national character varying(300)  NOT NULL
);



CREATE TABLE ReviewTaskTypes( Id tinyint NOT NULL
,Name national character varying(50)  NOT NULL
,Description national character varying(300)  NOT NULL
);



CREATE TABLE SuggestedEdits( Id int NOT NULL
,PostId int NOT NULL
,CreationDate datetime 
,ApprovalDate datetime 
,RejectionDate datetime 
,OwnerUserId int 
,Comment national character varying(800)  
,Text national character varying(MAX)  
,Title national character varying(250)  
,Tags national character varying(250)  
,RevisionGUID uniqueidentifier 
);



CREATE TABLE SuggestedEditVotes( Id int NOT NULL
,SuggestedEditId int NOT NULL
,UserId int NOT NULL
,VoteTypeId tinyint NOT NULL
,CreationDate datetime NOT NULL
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
,CreationDate datetime NOT NULL
,OwnerUserId int NOT NULL
,AutoRenameCount int NOT NULL
,LastAutoRename datetime 
,Score int NOT NULL
,ApprovedByUserId int 
,ApprovalDate datetime 
);



CREATE TABLE Users( Id int NOT NULL
,Reputation int NOT NULL
,CreationDate datetime NOT NULL
,DisplayName national character varying(40)  
,LastAccessDate datetime NOT NULL
,WebsiteUrl national character varying(200)  
,Location national character varying(100)  
,AboutMe national character varying(MAX)  
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
,VoteTypeId tinyint NOT NULL
,UserId int 
,CreationDate datetime 
,BountyAmount int 
);



CREATE TABLE VoteTypes( Id tinyint NOT NULL
,Name national character varying(50)  NOT NULL
);
