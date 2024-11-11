namespace PetRescueFE.Pages.Events;

public class EventUrlProfile
{
    public static readonly string BASE_URL = "http://localhost:7297/api/";
    public static readonly string BASE_URL_S = "https://localhost:7297/api/";
    public static readonly string GETS = "events/";
    public static readonly string GETS_P = "events/p";
    public static readonly string GET_DETAIL = "events/";
    public static readonly string POST_CREATE = "events/";
    public static readonly string PUT_UPDATE = "events/";
    public static readonly string DELETE = "events/";

    public static readonly string GET_SHELTER = "shelter";
    public static readonly string GET_SHELTER_BY_USER_ID = "shelter/userId/";

}

public class Role4Event
{
    public static readonly string ADMIN = "d290f1ee-6c54-4b01-90e6-d701748f0851";
    public static readonly string SHELTER_OWNER = "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f";
    public static readonly string USER = "e7b8f3d2-4a2f-4c3b-8f4d-9c5d8a3e1b2c";
}