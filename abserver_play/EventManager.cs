using System;
using System.Collections.Generic;

namespace MatchmakingObserver
{
    // класс для управления подпиской.

    public class EventManager
    {
        // Словарь для хранения подписчиков.
        private readonly Dictionary<string, HashSet<IEventListener>> _listeners;

        public EventManager()
        {
            _listeners = new Dictionary<string, HashSet<IEventListener>>();
        }

        // Подписка на событие.
        public void Subscribe(string eventType, IEventListener listener)
        {
            // Если такого типа события ещё нет в словаре, создаём новую запись
            if (!_listeners.ContainsKey(eventType))
            {
                _listeners[eventType] = new HashSet<IEventListener>();
            }

            // Добавляем подписчика в множество 
            _listeners[eventType].Add(listener);

            Console.WriteLine($"EventManager:  Подписчик {listener.GetType().Name} подписался на событие {eventType}");
        }

        // Отписка от события.
        public void Unsubscribe(string eventType, IEventListener listener)
        {
            // Проверяем, есть ли подписчики на это событие
            if (_listeners.ContainsKey(eventType))
            {
                // Удаляем подписчика
                _listeners[eventType].Remove(listener);

                Console.WriteLine($"EventManager:  Подписчик {listener.GetType().Name} отписался от события {eventType}");
            }
        }


        // Оповещение всех подписчиков о событии..
        public void Notify(string eventType, MatchEventData eventData)
        {
            // Проверяем, есть ли подписчики на это событие
            if (_listeners.ContainsKey(eventType))
            {
                Console.WriteLine($"\nEventManager:  Оповещение о событии: {eventType}");

                // Проходим по всем подписчикам 
                foreach (var listener in _listeners[eventType].ToList())
                {
                    // Вызываем метод оповещения у подписчика
                    listener.OnEventOccurred(eventType, eventData);
                }
            }
            else
            {
                Console.WriteLine($"EventManager:  Нет подписчиков на событие {eventType}");
            }
        }

        // проверить, есть ли подписчики на событие
        public bool HasListeners(string eventType)
        {
            return _listeners.ContainsKey(eventType) && _listeners[eventType].Count > 0;
        }
    }
}