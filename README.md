# ReaSync
ReaSync is a small tool to synchronize two folders in a locking way. It's intended for use with Reaper DAW to collaborate with others via NextCloud, Dropbox and similar software products but it can be used for other cases where you need a locked checkout of multiple files for multiple users.

## Configuration
The tool is basically just a small dialog which each contributor needs to use in order to prevent synchronuous changes to the folder structure. You need to configure a local and a remote directory. The local directory is the one where you really work with your tool, Reaper for example. The remote directory is provided by your cloud-space and could be your dropbox folder, your next-cloud folder or a sub folder of such a synchronized directory. As a last step, you need to configure a user name. As there is no central user management, each contributor needs to use a unique name.

## Features
The second tab page of the tool provides the following possibilities:
* Refresh Status
  Determines the current status of the remote directory
* Get Latest
  Completely deletes the local folder, recreates it and copies everything from the remote folder to it
* Check out
  In addition to what Get Latest does, also creates a .lock file in the remote location to mark the content as being checked out
* Check in
  Copies everything which has changed from the local folder to the remote folder and removes the .lock file
  
The change detection is quite primitive as a MD5 hash is generated for each file and stored as .hashes file in the remote location. When checking in, new hashes are created and all files where either the hash is different or no hash is known, are copied.

## Known issues
I didn't care for file access error handling. I know. I will implement it. Later. Promised.

## Future
The project is in a quite early state so please inform me about further requirements and I'll try to integrate them.
