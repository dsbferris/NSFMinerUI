# NSFMinerUI

### What is this?
NSFMinerUI provides a simple GUI for the NSFMiner with additional features focusing on stability and maximum efficency.

### Core features
Target of NSFMinerUI is to provide stable 24/7 experience, with problems being caught automatically. (In best case ;)
It features:
* Overclocking of Memory
* Setting of a Powerlimit
* Rejected Shares Detection [RSD]
* DAG Generated Detection to apply Overclock [DGD]
* Automated restart of Miner or whole System
* Clean Epoch switching with new DAG
* Ping test for choosing best address/location of your pool
* (Event based system with custom actions)

### Limitations
Written in C# it currently onyl supports Windows.
As of I only own a RTX 3070 and no AMD Card, currently only NVIDIA is tested/supported.
Also, don't expect to get higher Hashrates, this ain't downloadmorehashrate.com...


### My intended use-case
Originally made for myself and a friend.
Both of us hooked ourself up with a new GPU (lucky we got one) and found out it would be profitable to mine ETH while sleeping or else being afk.
Doing this on our Gaming PC we tested a lot with overclocks and powerlimits, process priority and so on.
We also experienced mining >12hrs with 100% rejected shares, yes due to too high overclock, but hell, it is usually working and I want to squeeze out every little bit VMem Clock aka. Hashrate. 

### Usage
TODO when finished coding ;)


### Found a bug?
Feel free to open up an Issue here.

### But I want feature XY
1. Open up and Issue and tell me why you think this is an essential feature.
2. Clone, open up in Visual Studio and feel free to extend to your needs.
Note: I never really worked with Git and my coding experience is limited, be feel free to make pull requests if you want to share your extends.

### BIG THANKS
Thanks to NVOClock for providing NVAPI CLI to apply memory overclock.
Thanks to NSFMiner for making a "No Stinkin' Fees Miner", which also outputs to stdout.
Thanks to Nvidia for their nvidia-smi toolkit to read livetime from CLI. Also for making such powerful GPU's, but well, we pay for them ;)
Thanks to myself who had this great idea due to being extremly annoyed by mining a night with 100% rejected shares.

### License
I have 0 f***in clue. Do what you want...
