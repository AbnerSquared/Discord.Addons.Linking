<img src="./marketing/Icon.png" width="64" height="64" />

# Discord.Addons.Linking
[![NuGet](https://img.shields.io/nuget/vpre/Discord.Addons.Linking.svg?maxAge=2592000?style=plastic)](https://www.nuget.org/packages/Discord.Addons.Linking)

An extension for Discord.Net that can synchronize messages.

# Purpose
This extension was made primarily to enable the synchronization of multiple messages with the exact same content at once. This allows you to update a single parent message, which will then automatically update all linked children.

# Additions

### LinkedMessage
The `LinkedMessage` binds to messages that are not sent by the bot, and are instantly updated whenever that message is updated.

### LinkedUserMessage
The `LinkedUserMessage` binds to a message that was only sent from a bot, and can be manually modified at any point in time. Keep in mind that a `LinkedUserMessage` inherits the `LinkedMessage` class, which means that all methods that the `LinkedMessage` class contains can also be executed by a `LinkedUserMessage`.

# Methods

## Initializing a `LinkedMessage`
```cs
LinkedMessage linkedMessage = LinkedMessage.Create(Context.Message, LinkDeleteHandling.Source, Context.Client);
```
The parameters for the `LinkedMessage` structure requires an `IMessage` to bind to, a specified `LinkDeleteHandling` flag, and a `BaseSocketClient` to initialize this message for. The `BaseSocketClient` is used to automatically handle updates when the message is changed.

## Initializing a `LinkedUserMessage`
```cs
RestUserMessage message = await Context.Channel.SendMessageAsync("This is a test.");
LinkedUserMessage linkedUserMessage = LinkedUserMessage.Create(message, LinkDeleteHandling.Source);
```
The parameters for the `LinkedUserMessage` requires an `IUserMessage` to bind to, along with a specified `LinkDeleteHandling` flag. Although a `BaseSocketClient` isn't required to initialize a new `LinkedUserMessage`, it will not be able to automatically handle source deletion outside of the program. An example of this is when a user manually deletes the message from `Discord`.

## Updating a `LinkedUserMessage`
```cs
await linkedUserMessage.ModifyAsync(x => x.Content = "This is an updated message.");
```
This method is identical to the existing `ModifyAsync` methods that exist on `Discord` message classes. Using this will update all subscribed children from this method.

## Creating a child for a `LinkedMessage`
```cs
IUserMessage child = await linkedMessage.CloneAsync(Context.Channel);
```
This method initializes a new message to the specified `IChannel` that will automatically update whenever the parent message is updated. Likewise with other `Discord` methods, there is an optional parameter for `RequestOptions`. This method can be called multiple times in the same channel, but it will simply just initialize new children messages.

## Binding messages to a `LinkedMessage`
```cs
RestUserMessage newChild = await Context.Channel.SendMessageAsync("This is a message.");
bool isLinkSuccess = await linkedMessage.AddAsync(newChild);
```
As long as the binding message is an `IUserMessage`, the message can be linked to a `LinkedMessage`. This will automatically update the binding message to mimic the parent message as well, only if the method was successful.

## Unbinding children messages from a `LinkedMessage`
```cs
bool isSuccess = linkedMessage.Remove(messageId);
```
When this method is called, it will attempt to remove and unlink the specified message ID (`ulong`) from the collection of children. If it was successful, it will return a Boolean value as `true`. Otherwise, it will return `false`.

## Deleting a `LinkedMessage`
```cs
await linkedMessage.DeleteAsync();
```
This method will delete a `LinkedMessage`. The way a `LinkedMessage` is deleted is entirely based on the `LinkDeleteHandling` flag you set. If the specified flag is `LinkDeleteHandling.All`, every message that the `LinkedMessage` handles is deleted. This includes all children alongside the parent. Likewise, if the specified flag is `LinkDeleteHandling.Source`, all children within a `LinkedMessage` will be updated to display `[Original Message Deleted]`, while the source of the `LinkedMessage` is deleted.

# Credits
[**Discord.Net**](https://github.com/discord-net/Discord.Net) for the [original logo design](https://github.com/discord-net/Discord.Net/blob/dev/docs/marketing/logo/PackageLogo.png).
