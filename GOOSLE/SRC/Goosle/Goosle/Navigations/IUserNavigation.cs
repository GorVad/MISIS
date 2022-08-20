using Goosle.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Goosle.Navigations
{
    public interface IUserNavigation
    {
        /// <summary>
        /// Изменяет имя в шапке навигации.
        /// </summary>
        void ChangeHeaderName(string value);

        /// <summary>
        /// Открывает страницу профиля.
        /// </summary>
        void Profile_Click(object sender, RoutedEventArgs e);

        /// <summary>
        /// Открывает страницу расписания.
        /// </summary>
        void Shedule_Click(object sender, RoutedEventArgs e);

        /// <summary>
        /// Октрывает страницу статистики.
        /// </summary>
        void CovidStats_Click(object sender, RoutedEventArgs e);


        void HideProfileCOVID_Status();

        void ShowProfileCOVID_Status();
    }
}
