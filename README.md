# Kebechet

## Description

Kebechet is a web service solution for updating dynamic dns host addresses based on Microsoft Azure DNS zones. 
It is named after the egyptian goddess of "freshness".

This solution was developed because I was dissatisfied with the available providers after most of them discontinued their free service offers in the last few years.
So I just implemented my own.

## Setup

1. Set up an Azure dns zone
2. Set up a service principal
3. Deploy Kebechet, for example on an Azure app service
4. Configure router to transmit new IP addresses

## Usage

Just set up your router to transmit the new IP address after each successful reconnect.

## Device support

1. FRITZ!Box routers (tested on a 7490 model)
2. Scripts (like via PowerShell)

As the request is based on simple GET and POST requests, it can be assumed that most routers with dynamic dns support should work as well.

## License

[MIT License](/LICENSE)
