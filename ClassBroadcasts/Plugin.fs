namespace ClassBroadcasts

open System.Collections.Generic
open Exiled.API.Features
open Exiled.Events.EventArgs.Player
open PlayerRoles

type Config() =
    interface Exiled.API.Interfaces.IConfig with
        member val IsEnabled = true with get, set

        member val Debug = false with get, set
// solving a bug with serialization
    member val IsEnabled = true with get, set

    member val Debug = false with get, set

    member val Broadcasts = Dictionary<RoleTypeId, Broadcast>() with get, set

type ClassBroadcasts() =
    inherit Plugin<Config>()

    override this.Author = "Vladislav-CS"

    override this.Name = "ClassBroadcasts"

    override this.Prefix = this.Name

    override this.OnEnabled() = (
            Exiled.Events.Handlers.Player.Spawned.Subscribe(fun ev -> this.OnSpawned(ev))
            base.OnEnabled()
        )

    override this.OnDisabled() = (
            Exiled.Events.Handlers.Player.Spawned.Unsubscribe(fun ev -> this.OnSpawned(ev))
            base.OnDisabled()
        )

    member this.OnSpawned(ev: SpawnedEventArgs) = (
            if this.Config.Broadcasts.ContainsKey(ev.Player.Role.Type) then ev.Player.Broadcast(this.Config.Broadcasts.[ev.Player.Role.Type])
        )