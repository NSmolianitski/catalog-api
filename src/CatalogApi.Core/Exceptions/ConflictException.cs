﻿namespace CatalogApi.Core.Exceptions;

public class ConflictException(string message) : Exception(message);