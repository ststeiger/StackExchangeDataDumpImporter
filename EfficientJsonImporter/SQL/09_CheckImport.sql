
-- USE StackExchange


SELECT 
	 Id
	,Reputation
	,CreationDate
	,DisplayName
	,LastAccessDate
	,WebsiteUrl
	,Location
	,AboutMe
	,Views
	,UpVotes
	,DownVotes
	,ProfileImageUrl
	,EmailHash
	,Age
	,AccountId
FROM Users


SELECT 
	 Id
	,UserId
	,Name
	,Date
	,Class
	,TagBased
FROM Badges


SELECT 
	 Id
	,TagName
	,Count
	,ExcerptPostId
	,WikiPostId
FROM Tags


SELECT 
	 Id
	,PostTypeId
	,AcceptedAnswerId
	,ParentId
	,CreationDate
	,DeletionDate
	,Score
	,ViewCount
	,Body
	,OwnerUserId
	,OwnerDisplayName
	,LastEditorUserId
	,LastEditorDisplayName
	,LastEditDate
	,LastActivityDate
	,Title
	,Tags
	,AnswerCount
	,CommentCount
	,FavoriteCount
	,ClosedDate
	,CommunityOwnedDate
FROM Posts


SELECT 
	 Id
	,PostHistoryTypeId
	,PostId
	,RevisionGUID
	,CreationDate
	,UserId
	,UserDisplayName
	,Comment
	,Text
FROM PostHistory


SELECT 
	 Id
	,PostId
	,Score
	,Text
	,CreationDate
	,UserDisplayName
	,UserId
FROM Comments


SELECT 
	 Id
	,PostId
	,VoteTypeId
	,UserId
	,CreationDate
	,BountyAmount
FROM Votes 
