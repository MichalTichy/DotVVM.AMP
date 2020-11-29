# DotVVM.AMP
DotVVM.AMP allows easy automatic conversion of certain [DotVVM](https://github.com/riganti/dotvvm) pages to AMP pages. The page must meet several basic criteria to work without any further configurations / modifications:
1.	No postbacks ale allowed.
2.	No external JavaScript is present.
3.	All the CSS code is under 75 kB and does not include any !important directives.

## How to use on complex pages
When all those criteria are met, then the only thing left to do is to mark the route as AMP ready and enjoy lighting fast performance.
Event if you page does not fit the abovementioned criteria, then you can use DotVVM.AMP. The DotVVM.AMP can be set up to ignore invalid parts and the DotVVM view can be slightly modified to mark the sections of the page which will be included only in the DotVVM page or only in the AMP version. 

# How to use
Number of steps needed to use transform some of you DotVVM pages into AMP is small and they are easy to follow:
1)	Add [DotVVM.AMP nuget package](https://www.nuget.org/packages/DotVVM.AMP) into your application.
2)	Modify DotvvmStartup.cs
a)	Call AddDotvvmAmpSupport on IDotvvmServiceCollection instance.
In this method you can configure DotVVM.AMP. You can for example configure how invalid constructs are handled.
b)	Call AddDotvvmAmp on DotvvmConfiguration instance.
c)	Modify route table registrations for routes, for which you want to have their AMP version, to use AddWithAmp instead of default Add method.
3)	Profit
 
 
In case that the AMP version does not work as expected, then you can try following steps to get it up and running:
-	You can exclude unsupported parts via `<amp:Exclude>` and `<amp:Include>` controls.
-	You can use resource binding instead of value binding when possible.
-	You can set handling mode for the type of error you are getting from Throw to LogAndIgnore.
With this setting the error would be logged and DotVVM.AMP will try to solve or ignore the problem.
 

