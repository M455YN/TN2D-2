using UnityEngine;
public class RuchomaPlatforma : MonoBehaviour
{
    public float predkoscPoruszania = 5f; // Ustaw prêdkoœæ poruszania platformy
    public float odlegloscPoruszania = 5f; // Ustaw odleg³oœæ, jak¹ ma pokonaæ platforma

    private Vector3 poczatkowaPozycja; // Pocz¹tkowa pozycja platformy

    private void Start()
    {
        poczatkowaPozycja = transform.position; // Zapamiêtaj pocz¹tkow¹ pozycjê
    }

    private void Update()
    {
        // Oblicz now¹ pozycjê platformy
        float przesuniecie = Mathf.PingPong(Time.time * predkoscPoruszania, odlegloscPoruszania);
        Vector3 nowaPozycja = poczatkowaPozycja + new Vector3(przesuniecie, 0f, 0f);

        // Przesuñ platformê na now¹ pozycjê
        transform.position = nowaPozycja;
    }
}
