# MonoDevelop

Befriending C# and F#, befriending Windows and Linux, befriending Visual Studio and MonoDevelop.

The original purpose of this repository — that has been accomplished! — was to be able to develop, build, and run F# code invoking C# code, from both Windows and Linux, and from both Visual Studio and Xamarin Studio / MonoDevelop.

# Tricks

I found it important to switch the F# project to the .NET 4.5 framework.

The rest just worked.

# Setup

To install MonoDevelop, on Linux, via http://fsharp.org/use/linux/, run the following:

```
sudo apt-key adv --keyserver keyserver.ubuntu.com --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list

sudo apt-get update
sudo apt-get install mono-complete fsharp
```

To add F# support, on Linux, form https://github.com/fsharp/xamarin-monodevelop-fsharp-addin, do the following:

```
...

If so, just use it, no installation is required.
If not, install the F# Language Binding via the AddIn manager.
MonoDevelop/Xamarin Studio --> Add-in manager --> Gallery --> Language Bindings --> F# Language Binding
```

On Windows, may be obsolete since expliticy setting .NET to 4.5 for the F# project: but I've installed Visual F# Tools 3.1.2 via this link: http://www.microsoft.com/en-us/download/details.aspx?id=44011

My Linux setup:

```
MonoDevelop
Version 5.9.6
Installation UUID: d2dbe015-32be-4ece-b55d-427fa27f5c93
Runtime:
	Mono 4.0.4 (Stable 4.0.4.1/5ab4c0d Tue Aug 25 23:11:51 UTC 2015) (64-bit)
	GTK+ 2.24.23 (Ambiance theme)

Build Information
Build information unavailable

Operating System
Linux
Linux dima-i9 3.13.0-63-generic #103-Ubuntu SMP Fri Aug 14 21:42:59 UTC 2015 x86_64 x86_64 x86_64 GNU/Linux
```
