# RepoX
PCL Project attached.
Cross platform code fetches data from a web server using the System.Net.Http library, and displays it in a label.

It is possible to validate the certificate before the connection is established by implementing a ServerCertificateValidationCallback handler at the native platform level.  So far only attempted for iOS.

If this handler returns true data is fetched and displayed in a label.  

However if the handler creates a dialog to ask the user to validate the certificate, in this implementation, it is briefly displayed and then the main form is immediately re rendered over it, indicating a problem with the chosen method of diaplaying dialogs from the callback.

Note that it is not possible to call invoke the dialog using "await" because the callback delegate is defined by the Microsoft library and is not marked as "async"
