﻿using System.ComponentModel.DataAnnotations;

namespace SolarLab.Academy.Contracts.Users;

/// <summary>
/// Пользователь.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Идентификатор записи.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Отчество.
    /// </summary>
    public string MiddleName { get; set; }
    
    /// <summary>
    /// ФИО.
    /// </summary>
    public string FullName { get; set; }
    
    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Телефонный номер пользователя.
    /// </summary>
    /// 
    public string PhoneNumber {  get; set; }
    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    public string Email {  get; set; }

}