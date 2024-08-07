import { useEffect, useState } from 'react';

import { parseGreetingTime } from './greetingTime';
import { GetChatApplications, GetChatShareApplication } from '@/services/ChatApplicationService';

export const useGreeting = () => {

    const [greeting, setGreeting] = useState<'morning' | 'noon' | 'afternoon' | 'night'>();

    useEffect(() => {
        setGreeting(parseGreetingTime());
    }, []);

    return greeting;
};

export const useApplication = () => {
    const [application, setApplication] = useState();
    const query = new URLSearchParams(window.location.search);
    const sharedId = query.get('sharedId');
    const id = query.get('id');
    useEffect(() => {
        if (sharedId) {
            GetChatShareApplication(sharedId)
                .then((res) => {
                    setApplication(res);
                });
        } else if (id) {
            GetChatApplications(id)
                .then((res) => {
                    setApplication(res);
                });
        }
    }, []);

    return application;
}