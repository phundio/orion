﻿using System;
using System.Collections.Generic;
using Orion.GlobalOffensive.Objects;

namespace Orion.GlobalOffensive
{
	/// <summary>
	/// Manages entities within the game world.
	/// </summary>
	public class ObjectManager : NativeObject
	{
		// Obtain this dynamically from the game at a later stage.
		private int _capacity = 128;
		private readonly List<BaseEntity> _objects = new List<BaseEntity>();

		/// <summary>
		/// Gets the current objects in the game world.
		/// </summary>
		public IReadOnlyList<BaseEntity> Objects { get { return _objects; } } 

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectManager"/> class.
		/// </summary>
		/// <param name="baseAddress">The base address.</param>
		public ObjectManager(IntPtr baseAddress) : base(baseAddress)
		{
		}

		private DateTime _lastUpdate = DateTime.MinValue;
		public void Update()
		{
			// Throttle the updates a little - entities won't be changing that frequently.
			// Realistically we don't need to call this very often at all, as we only keep references to the actual
			// entities in the game, and only resolve their members when they're actually required.
			// Still, calling this twice per second won't hurt, and in the event of a player joining (e.g. a deathmatch),
			// being aware of an extra guy around in a timely manner does help.
			// 
			// Note: DateTime.Now has approximately a 10-15ms resolving time, so whatever you change this to,
			// don't bother checking for anything below that number.
			if (DateTime.Now - _lastUpdate < TimeSpan.FromMilliseconds(500))
				return;

			_lastUpdate = DateTime.Now;
		}
	}
}
