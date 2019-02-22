import i18n from "i18next";
import LanguageDetector from "i18next-browser-languagedetector";
import { initReactI18next } from 'react-i18next';

i18n
    // .use(LanguageDetector)
    .use(initReactI18next)
    .init({
        resources: {
            en: {
                translation: {
                    "helloTest": "Hello, {{name}}!",
                    "googleLogin": "Sign in with Google!",
                    "UserNotFound": "Could not find user with specified email and password",
                    "PlayerNotFound": "Player does not have privileges for selected game"
                }
            },
            pl: {
                translation: {

                }
            }
        },
        fallbackLng: "en",
        debug: false,
        
        keySeparator: false,

        interpolation: {
            escapeValue: false,
            formatSeparator: ","
        }
    });

export default i18n;