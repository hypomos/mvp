<img src="./images/hippo_837366.svg" width="100px" />

Hypomos shall one day be what I had in mind for several years now: It should simplify how we share and handle private media items (like photos and videos).

# Reasoning

Almost everyone has a SmartPhone nowadays and is taking photos and or videos with it. During holidays we might even use a "real" SLR camera.

If we enjoy holidays together with friends, we might want to share photos and videos captured with all these different devices from all the folks. 

How do we do that currently? we copy them around (OneDrive, GoogleDrive, thumb drive, etc.) and therefore need duplicate disk space everywhere. Furthermore cleanup of "not that good pictures" has to be done for every "copy" there is. And also from legal perspective, this is basically a nightmare: if one photographer decides to revoke access to the photos, he would have to ask / request to delete these pictures from all recipients (which they might or might not comply to).

# Idea

My idea is, to create a system (let's call it Hypomos) where everyone can login with their accounts (relatively open: could be any social media login),
add his / her own storage (e.g. OneDrive, Azure Blob Storage, GoogleDrive, etc.) where such media items usually get uploaded to (using sync apps on SmartPhones / Desktops).
Now the system regularly scans for or gets informed about file changes and collects corresponding metadata.
Users may then create "static" or "dynamic" collections, meaning:

* static: choose specific items
* dynamic: define a "filter query" which automatically collects all matching media items and puts them into a dynamic collection 
  (e.g. let Hypomos collect all items captured between start and end of holidays, uploaded from user X and using cameras Y and Z [with optional approval process])

Now media items are stored in users source (e.g. OneDrive) and if shared with friends, these friends do not have to download / copy the pictures - they get access to the original media items through the platform (kind of a Broker system).
Therefore if a user decides, that a certain media item should be removed, it gets removed from all "share recipients" (unless they downloaded it - which to be honest can't be fully prevented).

# Imagine:

* You are organizing an event (wedding?) and want attendees to share their gathered photos / videos. Create an "event" (containing a share-to Token),
  pass that "share-to Token" around, allowing everyone to save their media items in their own data source and share their (static / dynamic)
  collection to that "share-To Token". Then the event organization can approve which items should be visible overall.

* You are working as a photographer: before a shooting you create an "event", where you will share the photos to
  you can print a QR code containing the link to this "event-share" (including auth token?) and tell clients that they might get their photos 
  from there (in this case, we probably want this share to be valid only a certain amount of time, forcing clients to download the photos and thus freeing photographers storage)

* You are a family of four, everyone has a SmartPhone and two members of the family even use a conventional camera. How do you store "common" media items (which all family members might have access to) and where do you store "private" stuff (you know that stuff which not every family member needs to see..)?
  In Hypomos every family member would have his/her own storage(s) and could share dynamically all "family" media items (e.g. defined with labels) whereas private items stay private and no accidental "oops I uploaded it to the family storage" happens.
  
# More ideas

Hypomos should not only allow to reduce disk space overall. It could also provide some additional benefits:

* allow dia shows of certain collections
* measure statistics (skipped in dia show, liked, not liked, opened that many times, ...) which not only allows some housekeeping mechanisms
* create "automagic" collections (long time not seen, most liked, ...)
* calculate image similarity metrics (and therefore also allow "housekeeping" of very similar images)
* doing some image analysis (using cloud services), like classification and face recognition (if consent has been given by media item owner). Put these information into "searchable labels" (allowing dynamic collections)
* do housekeeping based on statistics, metadata and other information (above points) :)
* hot / not-so-hot storage: you might want to photoshop or manipulate "newer" content, but older content could be moved to cheaper cloud storage 
  (e.g. Azure Blob Storage) which does not integrate that well with a desktop OS. Thus the system could automatically move files from one storage to another one.
  still allowing users to mark certain photos as "have to be kept hot" so that it might get moved to e.g. OneDrive for simpler manipulation
* approval processes need to be possible on either side (sharing based on dynamic filter / receiving for event)
* if user once deleted a photo, and it get's re-uploaded again, Hypomos could detect (based on thumbprint) that this has already been marked as "unwanted", thus it could automatically get marked for housekeeping :)

 # Terminology

 * an "event" is a collection of media items, therefore could also be called "library"
 * a media item would be a photo or a video (maybe also audio only?)

---

<sub>[hippo](https://thenounproject.com/term/hippo/837366) by [Maria Kislitsina](https://thenounproject.com/bymasha/) from [the Noun Project](https://thenounproject.com/).</sub>
