# Good Game Deals

`Good Game Deals` is a Universal Windows Platform application for finding the best deals on the market for video games. 

## Getting Started

The project is still in development and has not been released to the Windows Store yet. To run the application you must build from source.

### Prerequisites

You will need to clone this repository onto your computer using [git](https://git-scm.com/downloads), have [Visual Studio 2017](https://www.visualstudio.com/downloads/) installed, and create a [Is There Any Deal](https://isthereanydeal.com/apps/) account to access their api. 

### Installing

Clone the repistory onto your computer

```
git clone https://github.com/drdgvhbh/GoodGameDeals.git
```

Create a new app from the `Is There Any deal` [page](https://isthereanydeal.com/apps/new/).

![app](https://i.imgur.com/5XETMtm.png)

Generate an api key.

![apiKey](https://i.imgur.com/bON6Ygj.png)

Add your key to the `ITAD` key in the `apiKeys.resw` file. Build the application and run!

![build](https://i.imgur.com/1IVjf1Q.png)


## Built With

* [Template10](https://github.com/Windows-XAML/Template10) - The template used
* [Unity Container](https://github.com/unitycontainer/unity) - Dependency Injection Framework
* [Rx](https://www.nuget.org/packages/System.Reactive/) - Reactive Extensions for C#
* [UWP Community Toolkit](https://github.com/Microsoft/UWPCommunityToolkit) - Provides additional functionalities to the UWP platform

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* [Is There Any Deal Api](https://itad.docs.apiary.io/)
* [SteamSpy Api](https://steamspy.com/api.php)
