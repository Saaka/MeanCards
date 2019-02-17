import i18n from "i18next";
import LanguageDetector from "i18next-browser-languagedetector";
import { initReactI18next } from 'react-i18next';

i18n
    .use(LanguageDetector)
    .use(initReactI18next)
    .init({
        resources: {
            en: {
                translation: {
                    "helloTest": "Hello, {{name}}!",
                    "UserNotFound": "Could not find user with specified email and password"
                }
            },
        },
        fallbackLng: "en",
        debug: true,

        keySeparator: false,

        interpolation: {
            escapeValue: false,
            formatSeparator: ","
        }
    });

export default i18n;