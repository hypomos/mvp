Hypomos is the project name for an idea I now have for several years. It should simplify how we share and handle private media items (like photos and videos).

# Reason
Almost everyone has a SmartPhone nowadays and is taking photos / videos with that. During holidays we might even use a dedicated photo camera - or two.
If we enjoy holidays together with friends, we might want to share photos and videos - how do we do that now? we copy them around (OneDrive, GoogleDrive, ThumbDrive, ...) and therefore need duplicate disk space everywhere.
Cleanup of "not that good pictures" has to be done for every "copy" there is.

# Idea
My idea is, to create a system where everyone can login (let's call it hypomos) with their Social accounts (no need to create an additional login),
add his / her own storage (e.g. GoogleDrive, OneDrive, Azure Blob Storage, ...) where such media items usually get uploaded to.
Now the system regularly scans for file changes (or listens to them) and updates metadata entries in a central DB.
Users may then create "static" or "dynamic" collections:
* static: choose specific items
* dynamic: define a "filter" which automatically collects all matching media items and puts them into a dynamic collection 
  (e.g. let's collect all items captured between start and end of holidays, uploaded from user X and using cameras Y and Z [with approval process])

Now media items are stored in users source (e.g. OneDrive) and if shared with friends, they do not have to download them - they get access to these media items (still in original OneDrive) through the platform.
Therefore if a user decides, that a certain media item should be removed, it gets removed from all "share recipients" (unless they downloaded it)

## imagine:
* you organize an event (wedding?) and want attendees to share their gathered photos / videos. Create an "event" (containing a share-to Token),
  pass that "share-to Token" around, allowing everyone to save their media items in their own data source and share their (static / dynamic)
  collection to that "share-To Token". Then the event organization can approve which items should be visible overall.
* you are working as a photographer: before you do a shooting, you create an "event", where you will share the photos to
  you can print a QR code containing the link to this "share" (including auth token?) and tell clients that they might get their photos 
  from there (in this case, we probably want this share to be valid only a certain amount of time, forcing clients to download the photos and thus freeing photographers storage)
* you are a family of four, everyone has a smartphone and a camera. How do you store "common" media items, where do you store "private" stuff?
  in this system, every family member could have his/her own OneDrive storage with all media items and share dynamically all "family" media items (e.g. defined with labels)
  
## more ideas
The system should not only allow to reduce disk space overall. It should also provide convenient functionalities:
* allow dia shows of certain collections
* create "magic" collections (long time not seen, most liked, ...)
* measure statistics (skipped in dia show, liked, not liked, opened that many times, ...) to allow some housekeeping mechanisms
* calculate image similarity metrics
* doing some image analysis (cloud services), like classification and face recognition (if consent has been given by user). Put these information into "searchable labels" (allowing dynamic collections)
* do housekeeping based on statistics, metadata and other information (above points) :)
* hot / cold storage: you might want to photoshop or manipulate "newer" content, but older content could be moved to cheaper cloud storage 
  (e.g. Azure Blob Storage) which does not integrate that well with a desktop OS. Thus the system could automatically move files from one storage to another one.
  still allowing user to mark certain photos as "have to be kept warm" so that it might get moved to e.g. OneDrive for simpler manipulation
* approval processes need to be possible on either side (sharing / receiving)
* if user once deleted a photo, and it get's reuploaded again, system could detect (based on thumbprint) that this has already been marked as "unwanted", thus it gets automatically marked for housekeeping :)

 # about terminology
 * an "event" is nothing else than a collection of media items, therefore could be called "Library"
 * a media item could be a photo or a video (maybe also audio only?)
 * 
