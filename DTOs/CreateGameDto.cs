﻿using System.ComponentModel.DataAnnotations;

namespace GameStore.DTOs;

public record class CreateGameDto(
    [Required][StringLength(50)] string Name, 
    [Required] int GenreId, 
    [Required][Range(1,100)] decimal Price,
    DateOnly ReleaseDate
    );
